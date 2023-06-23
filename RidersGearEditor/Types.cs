using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Linq;
using static RCMUtils;

namespace RidersGearEditor.Types
{
    // TODO Add big endian types

    public interface IGuiUsable
    {
        bool ParseRestricions();
        // Read a string input and try to apply for underlying type
        bool TryParseToCode(string str, out string code);
    }

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

    public interface IValueMemoryPair<T>
    {
        public abstract T ValueLE { get; set; }
        public abstract T ValueBE { get; set; }
        public abstract string ValueHexLE { get; set; }
        public abstract string ValueHexBE { get; set; }
    }

    public interface IValueOffsetPair<T> : IValueMemoryPair<T>
    {
        public abstract uint OffsetLE { get; set; }
        public abstract uint OffsetBE { get; set; }
        public abstract string OffsetHexLE { get; set; }
        public abstract string OffsetHexBE { get; set; }

        void MakeAdressAbsolute(uint baseAdress);
        IValueAddressPair<T> ToAbsoluteAdress(uint baseAdress);
    }

    public interface IValueAddressPair<T> : IValueMemoryPair<T>
    {
        public abstract IValueOffsetPair<T> Pair { get; set; }
        public abstract uint AddressLE { get; set; }
        public abstract uint AddressBE { get; set; }
        public abstract string AddressHexLE { get; set; }
        public abstract string AddressHexBE { get; set; }
    }



    public struct ByteValueOffsetPair : IValueOffsetPair<System.Byte>
    {
        public byte ValueLE { get => value; set => this.value = value; }
        public uint OffsetLE { get => offset; set => offset = value; }
        public byte ValueBE { get => value; set => this.value = value; }
        public uint OffsetBE { get => SwapEndian(offset); set => offset = SwapEndian(value); }
        public string ValueHexLE { get => ToHex(value); set => this.value = HexToByte(value); }
        public string ValueHexBE { get => ToHex(value); set => this.value = HexToByte(value); }
        public string OffsetHexLE { get => ToHex(offset); set => offset = HexToUInt32(value); }
        public string OffsetHexBE { get => ToHex(OffsetBE); set => offset = SwapEndian(HexToUInt32(value)); }

        private byte value;
        private uint offset;

        public ByteValueOffsetPair(uint offset, bool bigEndian = true)
        {
            value = 0;
            if (bigEndian)
                this.offset = SwapEndian(offset);
            else
                this.offset = offset;
        }

        public void MakeAdressAbsolute(uint baseAdress)
        {
            OffsetLE += baseAdress;
        }

        public IValueAddressPair<byte> ToAbsoluteAdress(uint baseAdress)
        {
            throw new NotImplementedException();
        }
    }

    public struct SByteValueOffsetPair : IValueOffsetPair<System.SByte>
    {
        public sbyte ValueLE { get => value; set => this.value = value; }
        public uint OffsetLE { get => offset; set => offset = value; }
        public sbyte ValueBE { get => value; set => this.value = value; }
        public uint OffsetBE { get => SwapEndian(offset); set => offset = SwapEndian(value); }
        public string ValueHexLE { get => ToHex(value); set => this.value = HexToSByte(value); }
        public string ValueHexBE { get => ToHex(value); set => this.value = HexToSByte(value); }
        public string OffsetHexLE { get => ToHex(offset); set => offset = HexToUInt32(value); }
        public string OffsetHexBE { get => ToHex(OffsetBE); set => offset = SwapEndian(HexToUInt32(value)); }

        private sbyte value;
        private uint offset;

        public SByteValueOffsetPair(uint offset, bool bigEndian = true)
        {
            value = 0;
            if (bigEndian)
                this.offset = SwapEndian(offset);
            else
                this.offset = offset;
        }

        public void MakeAdressAbsolute(uint baseAdress)
        {
            OffsetLE += baseAdress;
        }

        public IValueAddressPair<sbyte> ToAbsoluteAdress(uint baseAdress)
        {
            throw new NotImplementedException();
        }
    }

    public struct Int32ValueOffsetPair : IValueOffsetPair<System.Int32>
    {
        public int ValueLE { get => value; set => this.value = value; }
        public uint OffsetLE { get => offset; set => offset = value; }
        public int ValueBE { get => SwapEndian(value); set => this.value = SwapEndian(value); }
        public uint OffsetBE { get => SwapEndian(offset); set => offset = SwapEndian(value); }
        public string ValueHexLE { get => ToHex(value); set => this.value = HexToInt32(value); }
        public string ValueHexBE { get => ToHex(ValueBE); set => this.value = SwapEndian(HexToInt32(value)); }
        public string OffsetHexLE { get => ToHex(offset); set => offset = HexToUInt32(value); }
        public string OffsetHexBE { get => ToHex(OffsetBE); set => offset = SwapEndian(HexToUInt32(value)); }

        private int value;
        private uint offset;

        public Int32ValueOffsetPair(uint offset, bool bigEndian = true)
        {
            value = 0;
            if (bigEndian)
                this.offset = offset;
            else
                this.offset = offset;
        }

        public void MakeAdressAbsolute(uint baseAdress)
        {
            OffsetLE += baseAdress;
        }

        public IValueAddressPair<int> ToAbsoluteAdress(uint baseAdress)
        {
            throw new NotImplementedException();
        }
    }

    public struct UInt32ValueOffsetPair : IValueOffsetPair<System.UInt32>
    {
        public uint ValueLE { get => value; set => this.value = value; }
        public uint OffsetLE { get => offset; set => offset = value; }
        public uint ValueBE { get => SwapEndian(value); set => this.value = SwapEndian(value); }
        public uint OffsetBE { get => SwapEndian(offset); set => offset = SwapEndian(value); }
        public string ValueHexLE { get => ToHex(value); set => this.value = HexToUInt32(value); }
        public string ValueHexBE { get => ToHex(ValueBE); set => this.value = SwapEndian(HexToUInt32(value)); }
        public string OffsetHexLE { get => ToHex(offset); set => offset = HexToUInt32(value); }
        public string OffsetHexBE { get => ToHex(OffsetBE); set => offset = SwapEndian(HexToUInt32(value)); }
        private uint value;
        private uint offset;

        public UInt32ValueOffsetPair(uint offset, bool bigEndian = true)
        {
            value = 0;
            if (bigEndian)
                this.offset = SwapEndian(offset);
            else
                this.offset = offset;
        }

        public void MakeAdressAbsolute(uint baseAdress)
        {
            OffsetLE += baseAdress;
        }

        public IValueAddressPair<uint> ToAbsoluteAdress(uint baseAdress)
        {
            throw new NotImplementedException();
        }
    }

    public struct FloatValueOffsetPair : IValueOffsetPair<float>, IGuiUsable
    {
        public float ValueLE { get => value; set => this.value = value; }
        public uint OffsetLE { get => offset; set => offset = value; }
        public float ValueBE { get => SwapEndian(value); set => this.value = SwapEndian(value); }
        public uint OffsetBE { get => SwapEndian(offset); set => offset = SwapEndian(value); }
        public string ValueHexLE { get => ToHex(value); set => this.value = HexToFloat(value); }
        public string ValueHexBE { get => ToHex(ValueBE); set => this.value = SwapEndian(HexToFloat(value)); }
        public string OffsetHexLE { get => ToHex(offset); set => offset = HexToUInt32(value); }
        public string OffsetHexBE { get => ToHex(OffsetBE); set => offset = SwapEndian(HexToUInt32(value)); }
        private float value;
        private uint offset;

        public FloatValueOffsetPair(uint offset, bool bigEndian = true)
        {
            value = 0;
            if (bigEndian)
                this.offset = SwapEndian(offset);
            else
                this.offset = offset;
        }

        public void MakeAdressAbsolute(uint baseAdress) => OffsetLE += baseAdress;
        public IValueAddressPair<float> ToAbsoluteAdress(uint baseAdress) => new FloatValAddrPair(baseAdress, this);

        public bool ParseRestricions()
        {
            throw new NotImplementedException();
        }

        public bool TryParseToCode(string str, out string code)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region ValueAdressPairs
    public struct FloatValAddrPair : IValueAddressPair<float>
    {
        public IValueOffsetPair<float> Pair { get => pair; set => pair = (FloatValueOffsetPair)value; }
        public float ValueLE { get => Pair.ValueLE; set => Pair.ValueLE = value; }
        public float ValueBE { get => Pair.ValueBE; set => Pair.ValueBE = value; }
        public string ValueHexLE { get => Pair.ValueHexLE; set => Pair.ValueHexLE = value; }
        public string ValueHexBE { get => Pair.ValueHexBE; set => Pair.ValueHexBE = value; }
        public uint AddressLE { get => Pair.OffsetLE; set => Pair.OffsetLE = value; }
        public uint AddressBE { get => Pair.OffsetBE; set => Pair.OffsetBE = value; }
        public string AddressHexLE { get => Pair.OffsetHexLE; set => Pair.OffsetHexLE = value; }
        public string AddressHexBE { get => Pair.OffsetHexBE; set => Pair.OffsetHexBE = value; }
        private FloatValueOffsetPair pair;

        public FloatValAddrPair(uint baseAdress, FloatValueOffsetPair pair)
        {
            pair.MakeAdressAbsolute(baseAdress);
            this.pair = pair;
        }

        public FloatValAddrPair(uint absoluteAdress, float value)
        {
            pair = new FloatValueOffsetPair(absoluteAdress);
            pair.ValueLE = value;
        }

        public FloatValAddrPair(uint absoluteAdress)
        {
            pair = new FloatValueOffsetPair(absoluteAdress);
        }
    }
    #endregion
}