using System.Collections;
using Byte = RidersGearEditor.Types.Byte;
using SByte = RidersGearEditor.Types.SByte;
using Float = RidersGearEditor.Types.Float;
using UInt32 = RidersGearEditor.Types.UInt32;
using Int32 = RidersGearEditor.Types.Int32;
using RidersGearEditor.Types;

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
    internal class GearStats
    {
        // Syntax:  
        // <Offset> <Datatype>
        // <Description>

        // 0x0 32-Bitstring
        // Who can select the gear
        public UInt32 Selectability = new(0x0);
        // 0x4 8-Bit char
        // GearType (0 for Baord, 1 for Skates, 2 for Bike)
        public Byte GearType = new(0x4);
        /// <summary>
        /// 0x5 8-Bit char <para></para>
        /// Model (tends to crash if changed)
        /// </summary>
        public Byte Model = new(0x5);
        /// <summary>
        /// 0xC 32-Bit float
        /// Acceleration modifier
        /// </summary>
        public Float Acceleration = new(0xC);
        /// <summary>
        /// 0x10 32-Bit float
        /// SpeedMultiplier doesn't affect handling
        /// </summary>
        public Float SpeedMultiplier = new(0x10);
        /// <summary>
        /// 0x14 32-Bit float
        /// OffRoad factor
        /// </summary>
        public Float OffRoadSpeed = new(0x14);
        /// <summary>
        /// 0x18 32-Bit float
        /// Affects speed and handling
        /// </summary>
        public Float SpeedHandlingMultiplier = new(0x18);
        /// <summary>
        /// 0x1C 32-Bit float
        /// Weight affects bumping into people
        /// </summary>
        public Float Weight = new(0x1C);
        /// <summary>
        /// 0x23 8-Bit char
        /// bit 1 for speed, bit 2 for flight, bit 4 for power, bit 6 for flight+power, bit 7 for all
        /// </summary>
        public Byte ExtraAttribute = new(0x23);
        /// <summary>
        /// 0x24 32-Bit float
        /// (?) Speed loss while turning
        /// </summary>
        public Float SpeedLossByTurning = new(0x24);
        /// <summary>
        /// 0x28 32-Bit float
        /// (?) Speed based handling, also affects flight handeling 
        /// </summary>
        public Float HandlingSpeed = new(0x28);
        /// <summary>
        /// 0x2C 32-Bit float
        /// (?)
        /// </summary>
        public Float BackAxelHandling = new(0x2C);
        /// <summary>
        /// 0x30 32-Bit float
        /// (?)
        /// </summary>
        public Float FrontAxelHandling = new(0x30);
        /// <summary>
        /// 0x34 32-Bit float
        /// 
        /// </summary>
        public Float DriftingRadius = new(0x34);
        /// <summary>
        /// 0x38
        /// </summary>
        public Float DriftRotation = new(0x38);
        /// <summary>
        /// 0x3C
        /// </summary>
        public Float DriftRotationBackAxel = new(0x3C);
        /// <summary>
        /// 0x50
        /// </summary>
        public Int32 DriftDashChargeTimer = new(0x50);
        /// <summary>
        /// 0x54
        /// </summary>
        public Float TrickAirGainMultiplier = new(0x54);
        /// <summary>
        /// 0x58
        /// </summary>
        public Float ShortcutAirGainMultiplier = new(0x58);
        /// <summary>
        /// 0x5C
        /// </summary>
        public Float QTEAirGainMultiplier = new(0x5C);
        /// <summary>
        /// 0x60
        /// </summary>
        public UInt32 SpecialFlags = new(0x60);
        /// <summary>
        /// 0x64
        /// </summary>
        public Float JumpChargeAirMultiplier = new(0x64);
        #region GearLevelStats
        public GearLevelStats Level1Stats;
        public GearLevelStats Level2Stats;
        public GearLevelStats Level3Stats;
        #endregion

        #region ShownStats
        public SByte ShownDashStat;
        public SByte ShownLimitStat;
        public SByte ShownPowerStat;
        public SByte ShownCorneringStat;
        #endregion

        #region Exhaust
        public Float ExaustTrailWidth;
        public Float SecondExhaustTrailWidth;
        public Float ExhaustTrailOffsetX;
        public Float ExhaustTrailOffsetY;
        public Float ExhaustTrailOffsetZ;
        public Float SecondExhaustTrailOffsetX;
        public Float SecondExhaustTrailOffsetY;
        public Float SecondExhaustTrailOffsetZ;
        public Float ExhaustTrailFlags;
        public Float ExhaustTrailWidthTricks;
        public Float SecondExhaustTrailWidthTricks;
        public Float ExhaustTrailOffsetXTricks;
        public Float ExhaustTrailOffsetYTricks;
        public Float ExhaustTrailOffsetZTricks;
        public Float SecondExhaustTrailOffsetXTricks;
        public Float SecondExhaustTrailOffsetYTricks;
        public Float SecondExhaustTrailOffsetZTricks;
        #endregion
    }

    public class GearLevelStats
    {
        /// <summary>
        /// 0x0
        /// </summary>
        public UInt32 MaxAir = new(0x0);
        /// <summary>
        /// 0x4
        /// </summary>
        public UInt32 AirDrain = new(0x4);
        /// <summary>
        /// 0x8
        /// </summary>
        public UInt32 DriftAirCost = new(0x8);
        /// <summary>
        /// 0xC
        /// </summary>
        public UInt32 BoostCost = new(0xC);
        /// <summary>
        /// 0x10
        /// </summary>
        public UInt32 TornadoCost = new(0x10);
        /// <summary>
        /// 0x14
        /// </summary>
        public Float DriftDashSpeed = new(0x14);
        /// <summary>
        /// 0x18
        /// </summary>
        public Float BoostSpeed = new(0x18);
    }

    [Flags]
    public enum GearSpecialFlags
    {
        // TODO Add propper comments
        IgnoreTurbulence = 0x_0000_0001,
        MaxJump = 0x_0000_0002,
        Ice = 0x_0000_0004,
        NoBoost = 0x_0000_0010,
        AutoDrift = 0x_0000_0020,
        NoSpeedLossUphill = 0x_0000_0040,
        TornadoBoost = 0x_0000_0080,
        NoSpeedLossChargingJump = 0x_0000_0100,
        FirstPlaceDoubleRings = 0x_0000_0200,
        RingGear = 0x_0000_0400,
        DisableAttack = 0x_0000_0800,
        AlwaysAttack = 0x_0000_1000,
        NoType = 0x_0000_2000,
        NoPits = 0x_0000_4000,
        AirStart30Percent = 0x_0000_8000,
        AirStart50Percent = 0x_0001_0000,
        IceImmunity = 0x_0002_0000,
        LighBoardEffect = 0x_0004_0000,
        StickyFingers = 0x_0008_0000,
    }

    public enum GearAdress : uint
    {
        DefaultGear = 0x805E5F40,
        HighBooster = 0x805E6110,
        AutoSlider = 0x805E62E0,
        PowerfulGear = 0x805E64B0,
        Fastest = 0x805E6680,
        TurboStar = 0x805E6850,
        SpeedBalancer = 0x805E6A20,
        BlueStar2 = 0x805E6BF0,
        Access = 0x805E6DC0,
        Beginner = 0x805E6F90,
        Accelerator = 0x805E7160,
        TrapGear = 0x805E7330,
        LightBoard = 0x805E7500,
        SlideBooser = 0x805E76D0,
        legend = 0x805E78A0,
        MagicCarpet = 0x805E7A70,
        AirBroom = 0x805E7C40,
        Hovercraft = 0x805E7E10,
        ChaosEmerald = 0x805E7FE0,
        Faster = 0x805E81B0,
        Gambler = 0x805E8380,
        PowerGear = 0x805E8550,
        OpaOpa = 0x805E8720,
        TheCrazy = 0x805E88F0,
        Berserker = 0x805E8AC0,
        ERider = 0x805E8C90,
        AirTank = 0x805E8E60,
        HeavyBike = 0x805E9030,
        Destroye = 0x805E9200,
        Omnipotence = 0x805E93D0,
        CoverS = 0x805E95A0,
        CoverF = 0x805E9770,
        CoverP = 0x805E9940,
        HangOn = 0x805E9B10,
        SuperHangOn = 0x805E9CE0,
        Darkness = 0x805E9EB0,
        Grinder = 0x805EA080,
        AdvantageS = 0x805EA250,
        AdvantageF = 0x805EA420,
        AdvantageP = 0x805EA5F0,
        Cannonball = 0x805EA7C0
    }

    public static class Utils
    {
        public static float ToSpeedometerSpeed(float ridersSpeed) => 216.0f * ridersSpeed;
        public static float ToRidersSpeed(float speedometerSpeed) => speedometerSpeed / 216.0f;
    }

}