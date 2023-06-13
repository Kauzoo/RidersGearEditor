using System.Collections;

namespace RidersGearEditor
{
    internal class ExtremeGear
    {
        public Hex32String adress;
        public GearStats gearStats;
    }

    // Contains general gear stats
    /*
    Gear struct is 0x1D0 in Size
    */
    internal class GearStats : IEnumerator
    {
        // Syntax:  
        // <Offset> <Datatype>
        // <Description>

        // 0x0 32-Bitstring
        // Who can select the gear
        public uint Selectability;
        // 0x4 8-Bit char
        // GearType (0 for Baord, 1 for Skates, 2 for Bike)
        public byte GearType;
        /// <summary>
        /// 0x5 8-Bit char <para></para>
        /// Model (tends to crash if changed)
        /// </summary>
        public byte Model;
        /// <summary>
        /// 0xC 32-Bit float
        /// Acceleration modifier
        /// </summary>
        public float Acceleration;
        /// <summary>
        /// 0x10 32-Bit float
        /// SpeedMultiplier doesn't affect handling
        /// </summary>
        public float SpeedMultiplier;
        /// <summary>
        /// 0x14 32-Bit float
        /// OffRoad factor
        /// </summary>
        public float OffRoadSpeed;
        /// <summary>
        /// 0x18 32-Bit float
        /// Affects speed and handling
        /// </summary>
        public float SpeedHandlingMultiplier;
        /// <summary>
        /// 0x1C 32-Bit float
        /// Weight affects bumping into people
        /// </summary>
        public float Weight;
        /// <summary>
        /// 0x23 8-Bit char
        /// bit 1 for speed, bit 2 for flight, bit 4 for power, bit 6 for flight+power, bit 7 for all
        /// </summary>
        public byte ExtraAttribute;
        /// <summary>
        /// 0x24 32-Bit float
        /// (?) Speed loss while turning
        /// </summary>
        public float SpeedLossByTurning;
        /// <summary>
        /// 0x28 32-Bit float
        /// (?) Speed based handling, also affects flight handeling 
        /// </summary>
        public float HandlingSpeed;
        /// <summary>
        /// 0x2C 32-Bit float
        /// (?)
        /// </summary>
        public float BackAxelHandling;
        /// <summary>
        /// 0x30 32-Bit float
        /// (?)
        /// </summary>
        public float FrontAxelHandling;
        /// <summary>
        /// 0x34 32-Bit float
        /// 
        /// </summary>
        public float DriftingRadius;
        /// <summary>
        /// 0x38
        /// </summary>
        public float DriftRotation;
        /// <summary>
        /// 0x3C
        /// </summary>
        public float DriftRotationBackAxel;
        /// <summary>
        /// 0x50
        /// </summary>
        public float DriftDashChargeTimer;
        /// <summary>
        /// 0x54
        /// </summary>
        public float TrickAirGainMultiplier;
        /// <summary>
        /// 0x58
        /// </summary>
        public float ShortcutAirGainMultiplier;
        /// <summary>
        /// 0x5C
        /// </summary>
        public float QTEAirGainMultiplier;
        /// <summary>
        /// 0x60
        /// </summary>
        public uint SpecialFlags;
        /// <summary>
        /// 0x64
        /// </summary>
        public float JumpChargeAirMultiplier;
        #region GearLevelStats
        public GearLevelStats Level1Stats;
        public GearLevelStats Level2Stats;
        public GearLevelStats Level3Stats;
        #endregion

        #region ShownStats
        public sbyte ShownDashStat;
        public sbyte ShownLimitStat;
        public sbyte ShownPowerStat;
        public sbyte ShownCorneringStat;
        #endregion

        #region Exhaust
        public float ExaustTrailWidth;
        public float SecondExhaustTrailWidth;
        public float ExhaustTrailOffsetX;
        public float ExhaustTrailOffsetY;
        public float ExhaustTrailOffsetZ;
        public float SecondExhaustTrailOffsetX;
        public float SecondExhaustTrailOffsetY;
        public float SecondExhaustTrailOffsetZ;
        public uint ExhaustTrailFlags;
        public float ExhaustTrailWidthTricks;
        public float SecondExhaustTrailWidthTricks;
        public float ExhaustTrailOffsetXTricks;
        public float ExhaustTrailOffsetYTricks;
        public float ExhaustTrailOffsetZTricks;
        public float SecondExhaustTrailOffsetXTricks;
        public float SecondExhaustTrailOffsetYTricks;
        public float SecondExhaustTrailOffsetZTricks;

        public object Current => throw new NotImplementedException();

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public struct GearLevelStats
    {
        /// <summary>
        /// 0x0
        /// </summary>
        public uint MaxAir;
        /// <summary>
        /// 0x4
        /// </summary>
        public uint AirDrain;
        /// <summary>
        /// 0x8
        /// </summary>
        public uint DriftAirCost;
        /// <summary>
        /// 0xC
        /// </summary>
        public uint BoostCost;
        /// <summary>
        /// 0x10
        /// </summary>
        public uint TornadoCost;
        /// <summary>
        /// 0x14
        /// </summary>
        public float DriftDashSpeed;
        /// <summary>
        /// 0x18
        /// </summary>
        public float BoostSpeed;
    }

    public static class Utils
    {
        public static float ToSpeedometerSpeed(float ridersSpeed) => 216.0f * ridersSpeed;
        public static float ToRidersSpeed(float speedometerSpeed) => speedometerSpeed / 216.0f;
    }

}