namespace ShapeFiles;

/// <summary>
/// Byte
/// Position  Field Value Type Number Order
/// Byte 0 Shape         Type 11 int 1 Little
/// Byte 4 X         X double 1 Little
/// Byte 12 Y        Y double 1 Little
/// Byte 20 Z        Z double 1 Little
/// Byte 28 Measure  M double 1 Little
/// </summary>
public sealed record PointZ : Geometry
{
    public override ShapeType ShapeType { get { return ShapeType.PointZ; } }

    public double X { get; } // X coordinate
    public double Y { get; } // Y coordinate
    public double Z { get; } // Z coordinate
    public double M { get; } // Measure

    public PointZ(double x,
                  double y,
                  double z,
                  double m)
    {
        X = x;
        Y = y;
        Z = z;
        M = m;
    }

    public PointZ(BinaryReader reader)
    {
        X = reader.ReadDouble();
        Y = reader.ReadDouble();
        Z = reader.ReadDouble();
        M = reader.ReadDouble();
    }
}