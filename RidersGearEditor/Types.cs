using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using static RCMUtils;

namespace RidersGearEditor.Types
{
    #region HexString
    public interface IHexString
    {
        private static readonly string hexMarkerSmall = "0x";
        private static readonly string hexMarkerBig = "0X";
        private static readonly string[] hexMarkers = { "0x", "0X" };
        private static readonly char[] hexChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'A', 'B', 'C', 'D', 'E', 'F' };

        public char[] Value { get; set; }

        public static bool IsHexString(string str)
        {
            foreach (var d in str)
            {
                if (!hexChars.Contains<char>(d))
                    return false;
            }
            return true;
        }

        public static string RemoveHexMarker(string str) => (str.StartsWith(hexMarkerSmall) || str.StartsWith(hexMarkerBig)) ? str[2..] : str;

        bool ValidateHexString(string str);
    }

    public struct Hex8String : IHexString
    {
        public char[] Value { get => value; set => this.value = value; }
        public char[] value = new char[2];
 
        public Hex8String(char[] value)
        {
            // TODO
        }

        public Hex8String(string value)
        {
            // TODO
        }

        public bool ValidateHexString(string str)
        {
            str = IHexString.RemoveHexMarker(str);
            return str.Length == 2 && IHexString.IsHexString(str);
        }
    }
    public struct Hex16String : IHexString
    {
        public char[] Value { get => value; set => this.value = value; }
        public char[] value = new char[4];

        public Hex16String(char[] value)
        {
            // TODO
        }

        public Hex16String(string value)
        {
            // TODO
        }

        public bool ValidateHexString(string str)
        {
            str = IHexString.RemoveHexMarker(str);
            return str.Length == 4 && IHexString.IsHexString(str);
        }
    }
    public struct Hex32String : IHexString
    {
        public char[] Value { get => value; set => this.value = value; }
        public char[] value = new char[8];

        public Hex32String(char[] value)
        {
            // TODO
        }

        public Hex32String(string val)
        {
            // TODO
        }

        public bool ValidateHexString(string str)
        {
            str = IHexString.RemoveHexMarker(str);
            return str.Length == 8 && IHexString.IsHexString(str);
        }

        public override string ToString()
        {
            string str = "";
            foreach (char c in value)
            {
                str += c;
            }
            return str;
        }
    }
    #endregion

    public static class NumberExtensions
    {
        public static byte[] GetBytes(this uint num) => BitConverter.GetBytes(num);
    }

    #region TypeWrappers
    public interface IByteArray
    {
        public byte[] Bytes { get; set; }
    }

    public static class RCMNumberUtils
    {
        public static void SwapEndian(this IByteArray number) => number.Bytes.Reverse();
    }

    public struct Int32_s : IByteArray
    {
        public byte[] Bytes { get => BitConverter.GetBytes(val); set => val = BitConverter.ToInt32(value); }
        int val;
        public Int32_s(int val) { this.val = val; }
        public Int32_s(byte[] val) { this.val = BitConverter.ToInt32(val); }
    }

    public interface IValueOffsetPair<T>
    {
        T ValueLE { get; set; }
        T ValueBE { get; set; }
        uint OffsetLE { get; set; }
        uint OffsetBE { get; set; }
        string ValueHexLE { get; set; }
        string ValueHexBE { get; set; }
        string OffsetHexLE { get; set; }
        string OffsetHexBE { get; set; }


        public static void ToAbsoluteAdress(uint baseAdress, IValueOffsetPair<T> valueOffsetPair)
        {
            valueOffsetPair.OffsetLE += baseAdress;
        }

        void MakeAdressAbsolute(uint baseAdress);


    }

    public class IValueAdressPair<T> where T : IByteArray
    {
    }

    public struct Byte : IValueOffsetPair<System.Byte>
    {
        public byte ValueLE { get => value; set => this.value = value; }
        public uint OffsetLE { get => offset; set => offset = value; }
        public byte ValueBE { get => value; set => this.value = value; }
        public uint OffsetBE { get => SwapEndian(offset); set => this.offset = SwapEndian(value); }
        public string ValueHexLE { get => ToHex(value); set => this.value = HexToByte(value); }
        public string ValueHexBE { get => ToHex(value); set => this.value = HexToByte(value); }
        public string OffsetHexLE { get => ToHex(offset); set => offset = HexToUInt32(value); }
        public string OffsetHexBE { get => ToHex(OffsetBE); set => offset = SwapEndian(HexToUInt32(value)); }

        private byte value;
        private uint offset;

        public Byte(uint offset, bool bigEndian = true)
        {
            if (bigEndian)
                OffsetBE = offset;
            else
                OffsetLE = offset;
        }

        public void MakeAdressAbsolute(uint baseAdress)
        {
            IValueOffsetPair<System.Byte>.ToAbsoluteAdress(baseAdress, this);
        }
    }

    public struct SByte : IValueOffsetPair<System.SByte>
    {
        public sbyte ValueLE { get => value; set => this.value = value; }
        public uint OffsetLE { get => offset; set => offset = value; }
        public sbyte ValueBE { get => value; set => this.value = value; }
        public uint OffsetBE { get => SwapEndian(offset); set => this.offset = SwapEndian(value); }
        public string ValueHexLE { get => ToHex(value); set => this.value = HexToSByte(value); }
        public string ValueHexBE { get => ToHex(value); set => this.value = HexToSByte(value); }
        public string OffsetHexLE { get => ToHex(offset); set => offset = HexToUInt32(value); }
        public string OffsetHexBE { get => ToHex(OffsetBE); set => offset = SwapEndian(HexToUInt32(value)); }

        private sbyte value;
        private uint offset;

        public SByte(uint offset, bool bigEndian = true)
        {
            if (bigEndian)
                OffsetBE = offset;
            else
                OffsetLE = offset;
        }

        public void MakeAdressAbsolute(uint baseAdress)
        {
            IValueOffsetPair<System.SByte>.ToAbsoluteAdress(baseAdress, this);
        }
    }

    public struct Int32 : IValueOffsetPair<System.Int32>
    {
        public int ValueLE { get => value; set => this.value = value; }
        public uint OffsetLE { get => offset; set => offset = value; }
        public int ValueBE { get => SwapEndian(value); set => this.value = SwapEndian(value); }
        public uint OffsetBE { get => SwapEndian(offset); set => this.offset = SwapEndian(value); }
        public string ValueHexLE { get => ToHex(value); set => this.value = HexToInt32(value); }
        public string ValueHexBE { get => ToHex(ValueBE); set => this.value = SwapEndian(HexToInt32(value)); }
        public string OffsetHexLE { get => ToHex(offset); set => offset = HexToUInt32(value); }
        public string OffsetHexBE { get => ToHex(OffsetBE); set => offset = SwapEndian(HexToUInt32(value)); }

        private int value;
        private uint offset;

        public Int32(uint offset, bool bigEndian = true)
        {
            if (bigEndian)
                OffsetBE = offset;
            else
                OffsetLE = offset;
        }

        public void MakeAdressAbsolute(uint baseAdress)
        {
            IValueOffsetPair<System.Int32>.ToAbsoluteAdress(baseAdress, this);
        }
    }

    public struct UInt32 : IValueOffsetPair<System.UInt32>
    {
        public uint ValueLE { get => value; set => this.value = value; }
        public uint OffsetLE { get => offset; set => offset = value; }
        public uint ValueBE { get => SwapEndian(value); set => this.value = SwapEndian(value); }
        public uint OffsetBE { get => SwapEndian(offset); set => this.offset = SwapEndian(value); }
        public string ValueHexLE { get => ToHex(value); set => this.value = HexToUInt32(value); }
        public string ValueHexBE { get => ToHex(ValueBE); set => this.value = SwapEndian(HexToUInt32(value)); }
        public string OffsetHexLE { get => ToHex(offset); set => offset = HexToUInt32(value); }
        public string OffsetHexBE { get => ToHex(OffsetBE); set => offset = SwapEndian(HexToUInt32(value)); }
        private uint value;
        private uint offset;

        public UInt32(uint offset, bool bigEndian = true)
        {
            if (bigEndian)
                OffsetBE = offset;
            else
                OffsetLE = offset;
        }

        public void MakeAdressAbsolute(uint baseAdress)
        {
            IValueOffsetPair<System.UInt32>.ToAbsoluteAdress(baseAdress, this);
        }
    }

    public struct Float : IValueOffsetPair<float>
    {
        public float ValueLE { get => value; set => this.value = value; }
        public uint OffsetLE { get => offset; set => offset = value; }
        public float ValueBE { get => SwapEndian(value); set => this.value = SwapEndian(value); }
        public uint OffsetBE { get => SwapEndian(offset); set => this.offset = SwapEndian(value); }
        public string ValueHexLE { get => ToHex(value); set => this.value = HexToFloat(value); }
        public string ValueHexBE { get => ToHex(ValueBE); set => this.value = SwapEndian(HexToFloat(value)); }
        public string OffsetHexLE { get => ToHex(offset); set => offset = HexToUInt32(value); }
        public string OffsetHexBE { get => ToHex(OffsetBE); set => offset = SwapEndian(HexToUInt32(value)); }
        private float value;
        private uint offset;

        public Float(uint offset, bool bigEndian = true)
        {
            if (bigEndian)
                OffsetBE = offset;
            else
                OffsetLE = offset;
        }

        public void MakeAdressAbsolute(uint baseAdress)
        {
            IValueOffsetPair<float>.ToAbsoluteAdress(baseAdress, this);
        }
    }
    #endregion


    public struct AddressValuePair
    {
        public Hex32String Address;
        public Hex32String Value;
    }

    public struct RAMGeckoCode
    {
        public string value;
        public RAMGeckoCode(AddressValuePair addressValuePair)
        {

        }
    }
}