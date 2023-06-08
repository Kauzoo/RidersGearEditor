namespace RidersGearEditor
{
    // Contains general gear stats
    /*
    Gear struct is 0x1D0 in Size
    */
    internal struct GearStats
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

        public string Acceleration;

    }
}