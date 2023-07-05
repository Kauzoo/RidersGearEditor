using System;

namespace RidersGearEditor.Types
{
    public static class BigEndianExtensions
    {
        public static HexStringBE ToHex(this Buint number) => Convert.ToHexString(number.Bytes);
    }

    public interface IBigEndianType : IByteArray
    {

    }

    
    public struct Buint : IBigEndianType
    {
        public uint Value { get => _val; set => _val = value; }
        public byte[] Bytes { get => BitConverter.GetBytes(_val); set => _val = BitConverter.ToUInt32(value); }

        private uint _val;

        public Buint(uint value)
        {
            _val = value;
        }

        public Buint(HexString str)
        {
            _val = BitConverter.ToUInt32(str.Bytes);
        }

        public static explicit operator uint(Buint value) => value._val.SwapEndian();
        public static explicit operator Buint(uint value) => new Buint(value.SwapEndian());
        public static explicit operator Buint(HexString value) => new Buint(value);
        public static explicit operator HexStringBE(Buint value) => new HexStringBE(value.Bytes);
    }
}