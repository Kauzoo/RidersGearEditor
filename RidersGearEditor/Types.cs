using System;

namespace RidersGearEditor
{
    public class HexString 
    {
        char[] HexChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'A', 'B', 'C', 'D', 'E', 'F' };

    }

    public class Hex8String : HexString
    {

    }
    public class Hex16String : HexString
    {

    }
    public class Hex32String : HexString
    {
        public char[] value = new char[8];
        public Hex32String()
        {

        }

        public Hex32String(char[] value) 
        {
            if (value)
        }

        public Hex32String(string val)
        {

        }

        public static bool ValidateString(string str)
        {
            
        }

    }

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