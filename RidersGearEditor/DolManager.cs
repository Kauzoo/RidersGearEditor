using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RidersGearEditor.Types;
using static System.Collections.Specialized.BitVector32;

namespace RidersGearEditor
{
    public class DolManager
    {

    }

    public interface IDolHeaderSection
    {
        public uint FileOffset { get; set; }
        public uint MemAddress { get; set; }
        public uint Size { get; set; }

        public static IDolHeaderSection CreateDolHeaderSection(IDolHeaderSection section, uint fileOffset, uint memAdress, uint size)
        {
            section.FileOffset = fileOffset;
            section.MemAddress = memAdress;
            section.Size = size;
            return section;
        }
    }
    public struct DataSection : IDolHeaderSection
    {
        public uint FileOffset { get; set; }
        public uint MemAddress { get; set; }
        public uint Size { get; set; }

        public DataSection(uint fileOffset, uint memAdress, uint size)
        {
            FileOffset = fileOffset;
            MemAddress = memAdress;
            Size = size;
        }
    }
    public struct TextSection : IDolHeaderSection
    {
        public uint FileOffset { get; set; }
        public uint MemAddress { get; set; }
        public uint Size { get; set; }

        public TextSection(uint fileOffset, uint memAdress, uint size)
        {
            FileOffset = fileOffset;
            MemAddress = memAdress;
            Size = size;
        }
    }

    public class DolInfo
    {
        public static readonly string DolExtension = ".dol"; 
        private readonly string _filePath;

        public TextSection[]? TextSections { get; private set; }
        public DataSection[]? DataSections { get; private set; }
        public uint MaxDolOffset { get; private set; }
        public uint MaxRAMAdress { get; private set; }

        public DolInfo(string? filePath)
        {
            if (filePath is null)
                throw new ArgumentNullException(nameof(filePath));
            if (!Path.IsPathFullyQualified(filePath))
                throw new ArgumentException();
            if (!Path.HasExtension(filePath) || Path.GetExtension(filePath) != DolExtension)
                throw new ArgumentException();
            this._filePath = filePath;
            ParseHeader();
        }

        /// <summary>
        /// Parse header for a .dol file
        /// Layout (Values in BigEndian):
        /// 0x0000 0x001B Text[0..6] sections File Positions
        /// 0x001C 0x0047 Data[0..10] sections File Positions
        /// 0x0048 0x0063 Text[0..6] sections Mem Address
        /// 0x0064 0x008F Data[0..10] sections Mem Address
        /// 0x0090 0x00AB Text[0..6] sections Sizes
        /// 0x00AC 0x00D7 Data[0..10] sections Sizes
        /// 0x00D8 0x04[] BSS Mem adress
        /// 0x00DC 0x04[] BSS Size
        /// 0x00E0 0x04[] Entry Point
        /// 0x00E4 0x1C[] unused
        /// 0x0100        Start of sections data (body)
        /// </summary>
        private void ParseHeader()
        {
            // This is a pretty direct adaption of the code in MCM
            // Important Note: Adresses are converted to LittleEndian here
            using (var stream = File.Open(_filePath, FileMode.Open))
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    var header = binaryReader.ReadBytes(256);
                    var textFileOffsets = ToAddress(header[..28]);          // 0x0000 - 0x001B | 7 * 4 Byte
                    var dataFileOffsets = ToAddress(header[28..72]);        // 0x001C - 0x0047 | 11 * 4 Byte
                    var textMemAddresses = ToAddress(header[72..0x64]);      // 0x0048 - 0x008F | 7 * 4 Byte
                    var dataMemAddresses = ToAddress(header[100..144]);     // 0x0064 - 0x008F | 11 * 4 Byte
                    var textSectionSizes = ToAddress(header[144..172]);     // 0x0090 - 0x00AB | 7 * 4 Byte
                    var dataSectionSizes = ToAddress(header[172..216]);     // 0x00AC - 0x00D7 | 11 * 4 Byte
                    var bssMemAddress = header.ToUInt32(216, true);         // 0x00D8 | 4 Byte
                    var bssSize = header.ToUInt32(220, true);               // 0x00DC | 4 Byte
                    var entryPoint = header.ToUInt32(224, true);            // 0x00E0 | 4 Byte

                    MaxDolOffset = 0u;
                    MaxRAMAdress = 0u;

                    // Create Text Sections
                    var tempTextSections = new List<TextSection>();
                    for (var i = 0; i < 6; i++)
                    {
                        // If any of these are 0 there are no more sections
                        if (textFileOffsets[i] == 0u || textMemAddresses[i] == 0u || textSectionSizes[i] == 0u)
                            break;
                        tempTextSections.Add(new TextSection(textFileOffsets[i], textMemAddresses[i], textSectionSizes[i]));
                        MaxDolOffset = (textFileOffsets[i] + textSectionSizes[i] > MaxDolOffset) ? textFileOffsets[i] + textSectionSizes[i] : MaxDolOffset;
                        MaxRAMAdress = (textMemAddresses[i] + textSectionSizes[i] > MaxRAMAdress) ? textMemAddresses[i] + textSectionSizes[i] : MaxRAMAdress;
                    }
                    TextSections = tempTextSections.ToArray();

                    // Create Data Sections
                    var tempDataSections = new List<DataSection>();
                    for (var i = 0; i < 10; i++)
                    {
                        // If any of these are 0 there are no more sections
                        if (dataFileOffsets[i] == 0u || dataMemAddresses[i] == 0u || dataSectionSizes[i] == 0u)
                            break;
                        tempDataSections.Add(new DataSection(dataFileOffsets[i], dataMemAddresses[i], dataSectionSizes[i]));
                        MaxDolOffset = (dataFileOffsets[i] + dataSectionSizes[i] > MaxDolOffset) ? dataFileOffsets[i] + dataSectionSizes[i] : MaxDolOffset;
                        MaxRAMAdress = (dataMemAddresses[i] + dataSectionSizes[i] > MaxRAMAdress) ? dataMemAddresses[i] + dataSectionSizes[i] : MaxRAMAdress;
                    }
                    DataSections = tempDataSections.ToArray();
                }
            }
        }

        public uint DolOffsetToRamAddress(uint dolOffset)
        {
            throw new NotImplementedException();
        }

        public uint RamAddressToDolOffset(uint ramAddress)
        {
            // ramAddress = RCMUtils.SwapEndian(ramAddress);   // TODO temporary
            foreach (var section in DataSections!)
            {
                if (ramAddress >= section.MemAddress && ramAddress < section.MemAddress + section.Size)
                {
                    var offsetInRam = ramAddress - section.MemAddress;
                    return section.FileOffset + offsetInRam;
                }
            }

            foreach (var section in TextSections!)
            {
                if (ramAddress >= section.MemAddress && ramAddress < section.MemAddress + section.Size)
                {
                    var offsetInRam = ramAddress - section.MemAddress;
                    return section.FileOffset + offsetInRam;
                }
            }
            return 0;
        }

        public string PrintValue(uint dolOffset)
        {
            using (var stream = File.Open(_filePath, FileMode.Open))
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    byte[] buffer = new byte[4];
                    var bytes = binaryReader.ReadBytes(1125000);
                    // 1072862
                    buffer = bytes[((int)dolOffset)..((int)dolOffset + 4)];
                    //binaryReader.Read(buffer, ((int)dolOffset), 4);
                    return Convert.ToHexString(buffer, 0, 4);
                }
            }
        }

        /// <summary>
        /// Convert a byte array to memory addresses
        /// Value are converted to Little endian
        /// </summary>
        /// <param name="seg"></param>
        /// <returns></returns>
        private static uint[] ToAddress(byte[] seg, bool swapEndian = true)
        {
            uint[] addr = new uint[seg.Length / 4];
            int index = 0;
            for (int i = 0; i < seg.Length; i += 4, index++)
            {
                addr[index] = swapEndian ? seg.ToUInt32(i, true) : seg.ToUInt32(i);
            }
            return addr;
        }

        /* public static void Main()
        {
            var dol = new DolInfo("E:\\source\\Riders_DX_2.0\\sys\\main.dol");
            // BigEndian:       010574
            // LittleEndian:    740501    
            Console.WriteLine(dol.PrintValue(dol.RamAddressToDolOffset(0x74_05_01)));
        } */
    }
}
