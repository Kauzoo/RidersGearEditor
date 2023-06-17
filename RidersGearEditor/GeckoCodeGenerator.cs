using RidersGearEditor.Types;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using static RCMUtils;
// TODO Migrate this to a full featured GeckCode Generator
namespace RidersGearEditor
{
    public class GeckoCodeGenerator
    {
        private enum CodeType : byte
        {
            BitWrite32 = 0x04
        }

        public static void BitsWrite8()
        {
            throw new NotImplementedException();
        }

        public static void BitsWrite16()
        {
            throw new NotImplementedException();
        }

        public static string BitsWrite32<T>(IValueOffsetPair<T> valueAdressPair)
        {
            return $"04{valueAdressPair.OffsetHexLE[2..]} {valueAdressPair.ValueHexBE}";
        }


        /*public static void Main()
        {
            ExtremeGear highBooster = new(GearAdress.HighBooster);
            highBooster.SpeedMultiplier.OffsetLE += highBooster.GearAdress;
            highBooster.SpeedMultiplier.ValueLE = SonicRidersUtils.ToRidersSpeed(100f);
            Console.WriteLine(BitsWrite32(highBooster.SpeedMultiplier));
            //ParseStruct(new GearStats());
        }*/

        public static void ParseStruct(IDataStruct dataStruct)
        {
            Type type = dataStruct.GetType();
            FieldInfo[] fieldInfos = type.GetFields();
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                Console.WriteLine(fieldInfo.Name);
            }
        }
    }

    public interface IGeckoCode
    {

    }

    public struct DirectRamWrite : IGeckoCode
    {
        public Hex32String Part1 { get; set; }
        public Hex32String Part2 { get; set; }
    }
}