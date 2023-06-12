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
        public string Selectability;
        // 0x4 8-Bit char
        // GearType (0 for Baord, 1 for Skates, 2 for Bike)
        public string GearType;
        /// <summary>
        /// 0x5 8-Bit char <para></para>
        /// Model (tends to crash if changed)
        /// </summary>
        public string Model;
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
        public string FrontAxelHandling;
        /// <summary>
        /// 0x34 32-Bit float
        /// 
        /// </summary>
        public string DriftingRadius;
        /// <summary>
        /// 0x38
        /// </summary>
        public string DriftRotation;
        /// <summary>
        /// 0x3C
        /// </summary>
        public string DriftRotationBackAxel;
        /// <summary>
        /// 0x50
        /// </summary>
        public string DriftDashChargeTimer;
        /// <summary>
        /// 0x54
        /// </summary>
        public string TrickAirGainMultiplier;
        /// <summary>
        /// 0x58
        /// </summary>
        public string ShortcutAirGainMultiplier;
        /// <summary>
        /// 0x5C
        /// </summary>
        public string QTEAirGainMultiplier;
        /// <summary>
        /// 0x60
        /// </summary>
        public string SpecialFlags;
        /// <summary>
        /// 
        /// </summary>
        public string JumpChargeAirMultiplier;
        public GearLevelStats Level1Stats;
        public GearLevelStats Level2Stats;
        public GearLevelStats Level3Stats;

        public string ShownDashStat;
        public string ShownLimitStat;
        public string ShownPowerStat;
        public string ShownCorneringStat;
        #region Exhaust
        public string ExaustTrailWidth;
        public string SecondExhaustTrailWidth;
        public string ExhaustTrailOffsetX;
        public string ExhaustTrailOffsetY;
        public string ExhaustTrailOffsetZ;
        public string SecondExhaustTrailOffsetX;
        public string SecondExhaustTrailOffsetY;
        public string SecondExhaustTrailOffsetZ;
        public string ExhaustTrailFlags;
        public string ExhaustTrailWidthTricks;
        public string SecondExhaustTrailWidthTricks;
        public string ExhaustTrailOffsetXTricks;
        public string ExhaustTrailOffsetYTricks;
        public string ExhaustTrailOffsetZTricks;
        public string SecondExhaustTrailOffsetXTricks;
        public string SecondExhaustTrailOffsetYTricks;
        public string SecondExhaustTrailOffsetZTricks;

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
        public string MaxAir;
        public string AirDrain;
        public string DriftAirCost;
        public string BoostCost;
        public string TornadoCost;
        public string DriftDashSpeed;
        public string BoostSpeed;
    }

}