using RidersGearEditor.Types;
using System;
using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using RidersGearEditor.Types;

public static class RCMUtils
{
    #region SwapEndian
    public static short SwapEndian(short number) => BitConverter.ToInt16(BitConverter.GetBytes(number).Reverse().ToArray());
    public static ushort SwapEndian(ushort number) => BitConverter.ToUInt16(BitConverter.GetBytes(number).Reverse().ToArray());
    public static int SwapEndian(int number) => BitConverter.ToInt32(BitConverter.GetBytes(number).Reverse().ToArray());
    public static uint SwapEndian(uint number) => BitConverter.ToUInt32(BitConverter.GetBytes(number).Reverse().ToArray());
    public static long SwapEndian(long number) => BitConverter.ToInt64(BitConverter.GetBytes(number).Reverse().ToArray());
    public static ulong SwapEndian(ulong number) => BitConverter.ToUInt64(BitConverter.GetBytes(number).Reverse().ToArray());
    public static float SwapEndian(float number) => BitConverter.ToSingle(BitConverter.GetBytes(number).Reverse().ToArray());
    public static double SwapEndian(double number) => BitConverter.ToDouble(BitConverter.GetBytes(number).Reverse().ToArray());
    public static byte[] SwapEndian(this byte[] bytes) => bytes.Reverse().ToArray();
    public static IByteArray SwapEndian(IByteArray number)
    {
        return number;
    }
    public static T SwapEndian<T>(T number) where T : IByteArray
    {
        return (T)SwapEndian((T)number);
    }
    #endregion

    public static uint ToUInt32(this byte[] bytes, int startIndex = 0, bool swapEndian = false) => (swapEndian) ? BitConverter.ToUInt32(bytes, startIndex) : BitConverter.ToUInt32(bytes.SwapEndian(), startIndex);

    public static string ToHex(short number) => Convert.ToHexString(BitConverter.GetBytes(number));
    public static string ToHex(ushort number) => Convert.ToHexString(BitConverter.GetBytes(number));
    public static string ToHex(int number) => Convert.ToHexString(BitConverter.GetBytes(number));
    public static string ToHex(this uint number, bool swapEndian = false) => (!swapEndian) ? Convert.ToHexString(BitConverter.GetBytes(number)) : Convert.ToHexString(BitConverter.GetBytes(number).Reverse().ToArray());
    public static string ToHex(long number) => Convert.ToHexString(BitConverter.GetBytes(number));
    public static string ToHex(ulong number) => Convert.ToHexString(BitConverter.GetBytes(number));
    public static string ToHex(float number) => Convert.ToHexString(BitConverter.GetBytes(number));
    public static string ToHex(double number) => Convert.ToHexString(BitConverter.GetBytes(number));

    public static byte HexToByte(string hex) => Convert.FromHexString(hex)[0];
    public static sbyte HexToSByte(string hex) => (sbyte) (Convert.FromHexString(hex)[0]);  // TODO idk if this actually works
    public static short HexToInt16(string hex) => BitConverter.ToInt16(Convert.FromHexString(hex));
    public static ushort HexToUInt16(string hex) => BitConverter.ToUInt16(Convert.FromHexString(hex));
    public static int HexToInt32(string hex) => BitConverter.ToInt32(Convert.FromHexString(hex));
    public static uint HexToUInt32(string hex) => BitConverter.ToUInt32(Convert.FromHexString(hex));
    public static long HexToInt64(string hex) => BitConverter.ToInt64(Convert.FromHexString(hex));
    public static ulong HexToUInt64(string hex) => BitConverter.ToUInt64(Convert.FromHexString(hex));
    public static float HexToFloat(string hex) => BitConverter.ToSingle(Convert.FromHexString(hex));
    public static double HexToDouble(string hex) => BitConverter.ToDouble(Convert.FromHexString(hex));
}