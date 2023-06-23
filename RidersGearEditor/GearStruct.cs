using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using ByteValueOffsetPair = RidersGearEditor.Types.ByteValueOffsetPair;
using SByteValueOffsetPair = RidersGearEditor.Types.SByteValueOffsetPair;
using FloatValueOffsetPair = RidersGearEditor.Types.FloatValueOffsetPair;
using UInt32ValueOffsetPair = RidersGearEditor.Types.UInt32ValueOffsetPair;
using Int32ValueOffsetPair = RidersGearEditor.Types.Int32ValueOffsetPair;
using RidersGearEditor.Types;

#nullable enable
namespace RidersGearEditor
{
    public interface IDataStruct
    {
        public static Dictionary<string, object?> GetFields(IDataStruct data)
        {
            var fields = new Dictionary<string, object?>();
            var info = data.GetType().GetFields();
            foreach (var field in info)
            {
                fields.Add(field.Name, field.GetValue(data));
            }
            return fields;
        }

        public static void Main()
        {
            var tmp = GetFields(new ExtremeGear(GearAdress.HighBooster).gearStats);
            Console.WriteLine("ja moin");
        }
    }

    public class ExtremeGear
    {
        public uint GearAdress { get; set; }
        public GearStats gearStats = new GearStats();

        public ExtremeGear(GearAdress gearAdress)
        {
            GearAdress = RCMUtils.SwapEndian((uint) gearAdress);
        }

        public ExtremeGear(uint gearAdress)
        {
            GearAdress = gearAdress;
        }
    }

    // Contains general gear stats
    /*
    Gear struct is 0x1D0 in Size
    */
    public class GearStats : IDataStruct
    {
        // Syntax:  
        // <Offset> <Datatype>
        // <Description>

        // 0x0 32-Bitstring
        // Who can select the gear
        public UInt32ValueOffsetPair selectability = new(0x0);
        // 0x4 8-Bit char
        // GearType (0 for Baord, 1 for Skates, 2 for Bike)
        public ByteValueOffsetPair GearType = new(0x4);
        /// <summary>
        /// 0x5 8-Bit char <para></para>
        /// Model (tends to crash if changed)
        /// </summary>
        public ByteValueOffsetPair Model = new(0x5);
        /// <summary>
        /// 0xC 32-Bit float
        /// Acceleration modifier
        /// </summary>
        public FloatValueOffsetPair Acceleration = new(0xC);
        /// <summary>
        /// 0x10 32-Bit float
        /// SpeedMultiplier doesn't affect handling
        /// </summary>
        public FloatValueOffsetPair SpeedMultiplier = new(0x10);
        /// <summary>
        /// 0x14 32-Bit float
        /// OffRoad factor
        /// </summary>
        public FloatValueOffsetPair OffRoadSpeed = new(0x14);
        /// <summary>
        /// 0x18 32-Bit float
        /// Affects speed and handling
        /// </summary>
        public FloatValueOffsetPair SpeedHandlingMultiplier = new(0x18);
        /// <summary>
        /// 0x1C 32-Bit float
        /// Weight affects bumping into people
        /// </summary>
        public FloatValueOffsetPair Weight = new(0x1C);
        /// <summary>
        /// 0x23 8-Bit char
        /// bit 1 for speed, bit 2 for flight, bit 4 for power, bit 6 for flight+power, bit 7 for all
        /// </summary>
        public ByteValueOffsetPair ExtraAttribute = new(0x23);
        /// <summary>
        /// 0x24 32-Bit float
        /// (?) Speed loss while turning
        /// </summary>
        public FloatValueOffsetPair SpeedLossByTurning = new(0x24);
        /// <summary>
        /// 0x28 32-Bit float
        /// (?) Speed based handling, also affects flight handeling 
        /// </summary>
        public FloatValueOffsetPair HandlingSpeed = new(0x28);
        /// <summary>
        /// 0x2C 32-Bit float
        /// (?)
        /// </summary>
        public FloatValueOffsetPair BackAxelHandling = new(0x2C);
        /// <summary>
        /// 0x30 32-Bit float
        /// (?)
        /// </summary>
        public FloatValueOffsetPair FrontAxelHandling = new(0x30);
        /// <summary>
        /// 0x34 32-Bit float
        /// 
        /// </summary>
        public FloatValueOffsetPair DriftingRadius = new(0x34);
        /// <summary>
        /// 0x38
        /// </summary>
        public FloatValueOffsetPair DriftRotation = new(0x38);
        /// <summary>
        /// 0x3C
        /// </summary>
        public FloatValueOffsetPair DriftRotationBackAxel = new(0x3C);
        /// <summary>
        /// 0x50
        /// </summary>
        public Int32ValueOffsetPair DriftDashChargeTimer = new(0x50);
        /// <summary>
        /// 0x54
        /// </summary>
        public FloatValueOffsetPair TrickAirGainMultiplier = new(0x54);
        /// <summary>
        /// 0x58
        /// </summary>
        public FloatValueOffsetPair ShortcutAirGainMultiplier = new(0x58);
        /// <summary>
        /// 0x5C
        /// </summary>
        public FloatValueOffsetPair QTEAirGainMultiplier = new(0x5C);
        /// <summary>
        /// 0x60
        /// </summary>
        public UInt32ValueOffsetPair SpecialFlags = new(0x60);
        /// <summary>
        /// 0x64
        /// </summary>
        public FloatValueOffsetPair JumpChargeAirMultiplier = new(0x64);
        #region GearLevelStats
        public GearLevelStats Level1Stats = new GearLevelStats();
        public GearLevelStats Level2Stats = new GearLevelStats();
        public GearLevelStats Level3Stats = new GearLevelStats();
        #endregion

        #region ShownStats
        public SByteValueOffsetPair ShownDashStat;
        public SByteValueOffsetPair ShownLimitStat;
        public SByteValueOffsetPair ShownPowerStat;
        public SByteValueOffsetPair ShownCorneringStat;
        #endregion

        #region Exhaust
        public FloatValueOffsetPair ExaustTrailWidth;
        public FloatValueOffsetPair SecondExhaustTrailWidth;
        public FloatValueOffsetPair ExhaustTrailOffsetX;
        public FloatValueOffsetPair ExhaustTrailOffsetY;
        public FloatValueOffsetPair ExhaustTrailOffsetZ;
        public FloatValueOffsetPair SecondExhaustTrailOffsetX;
        public FloatValueOffsetPair SecondExhaustTrailOffsetY;
        public FloatValueOffsetPair SecondExhaustTrailOffsetZ;
        public FloatValueOffsetPair ExhaustTrailFlags;
        public FloatValueOffsetPair ExhaustTrailWidthTricks;
        public FloatValueOffsetPair SecondExhaustTrailWidthTricks;
        public FloatValueOffsetPair ExhaustTrailOffsetXTricks;
        public FloatValueOffsetPair ExhaustTrailOffsetYTricks;
        public FloatValueOffsetPair ExhaustTrailOffsetZTricks;
        public FloatValueOffsetPair SecondExhaustTrailOffsetXTricks;
        public FloatValueOffsetPair SecondExhaustTrailOffsetYTricks;
        public FloatValueOffsetPair SecondExhaustTrailOffsetZTricks;
        #endregion
    }

    public class GearLevelStats
    {
        /// <summary>
        /// 0x0
        /// </summary>
        public UInt32ValueOffsetPair MaxAir = new(0x0);
        /// <summary>
        /// 0x4
        /// </summary>
        public UInt32ValueOffsetPair AirDrain = new(0x4);
        /// <summary>
        /// 0x8
        /// </summary>
        public UInt32ValueOffsetPair DriftAirCost = new(0x8);
        /// <summary>
        /// 0xC
        /// </summary>
        public UInt32ValueOffsetPair BoostCost = new(0xC);
        /// <summary>
        /// 0x10
        /// </summary>
        public UInt32ValueOffsetPair TornadoCost = new(0x10);
        /// <summary>
        /// 0x14
        /// </summary>
        public FloatValueOffsetPair driftDashSpeed = new(0x14);
        /// <summary>
        /// 0x18
        /// </summary>
        public FloatValueOffsetPair boostSpeed = new(0x18);
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

    public class GeneralStats
    {
        public FloatValueOffsetPair SSRankSpeed = new(0x805c2484);
        public FloatValueOffsetPair SRankSpeed = new(0x805c2480);
        public FloatValueOffsetPair XRankSpeed = new(0x805C2488);
    }

    public static class SonicRidersUtils
    {
        public static float ToSpeedometerSpeed(float ridersSpeed) => 216.0f * ridersSpeed;
        public static float ToRidersSpeed(float speedometerSpeed) => speedometerSpeed / 216.0f;
    }

}