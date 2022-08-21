namespace ShapeFiles;

/// <summary>
/// Byte
/// Position  Field Value Type Number Order
/// Byte 0 Shape         Type 21 int 1 Little
/// Byte 4 X         X double 1 Little
/// Byte 12 Y        Y double 1 Little
/// Byte 20 M        M double 1 Little
/// </summary>
public sealed record PointM : Geometry
{
    public override ShapeType ShapeType { get { return ShapeType.PointM; } }

    public double X { get; } // X coordinate
    public double Y { get; } // Y coordinate
    public double M { get; } // Measure

    public PointM(double x,
                  double y,
                  double m)
    {
        X = x;
        Y = y;
        M = m;
    }

    public PointM(BinaryReader reader)
    {
        X = reader.ReadDouble();
        Y = reader.ReadDouble();
        M = reader.ReadDouble();
    }
}