using System;

namespace RidersGearEditor.Types
{
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
    }

    #region TypeWrappers
    public interface IValueOffsetPair<T>
    {
        T Value { get; set; }
        uint Offset { get; set; }
    }

    public struct Byte : IValueOffsetPair<System.Byte>
    {
        public byte Value { get => value; set => this.value = value; }
        public uint Offset { get => offset; set => offset = value; }
        private byte value;
        private uint offset;

        public Byte(uint offset)
        {
            Offset = offset;
        }
    }

    public struct SByte : IValueOffsetPair<System.SByte>
    {
        public sbyte Value { get => value; set => this.value = value; }
        public uint Offset { get => offset; set => offset = value; }
        private sbyte value;
        private uint offset;

        public SByte(uint offset)
        {
            Offset = offset;
        }
    }

    public struct Int32 : IValueOffsetPair<System.Int32>
    {
        public int Value { get => value; set => this.value = value; }
        public uint Offset { get => offset; set => offset = value; }
        private int value;
        private uint offset;

        public Int32(uint offset)
        {
            Offset = offset;
        }
    }

    public struct UInt32 : IValueOffsetPair<System.UInt32>
    {
        public uint Value { get => value; set => this.value = value; }
        public uint Offset { get => offset; set => offset = value; }
        private uint value;
        private uint offset;

        public UInt32(uint offset)
        {
            Offset = offset;
        }
    }

    public struct Float : IValueOffsetPair<float>
    {
        public float Value { get => value; set => this.value = value; }
        public uint Offset { get => offset; set => offset = value; }
        private float value;
        private uint offset;

        public Float(uint offset)
        {
            Offset = offset;
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