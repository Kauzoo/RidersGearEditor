using System;

namespace RidersGearEditor;

[AttributeUsage (AttributeTargets.Field)]
public sealed class OffsetAttribute : Attribute
{
    public uint offset;

    public OffsetAttribute(uint offset) => this.offset = offset;
}

[AttributeUsage (AttributeTargets.Field)]
public sealed class AddressAttribute : Attribute
{
    public uint address;

    public AddressAttribute(uint address) => this.address = address;
}