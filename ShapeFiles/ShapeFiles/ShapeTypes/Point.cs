namespace ShapeFiles;

/// <summary>
/// Byte
/// Position      Field Value Type Number Order
/// Byte 0 Shape  Type 1 int 1 Little
/// Byte 4 X  X double 1 Little
/// Byte 12 Y Y double 1 Little
/// </summary>
public sealed record Point : Geometry
{
    public override ShapeType ShapeType { get { return ShapeType.Point; } }

    public double X { get; } // X coordinate
    public double Y { get; } // Y coordinate

    public Point(double x,
                 double y)
    {
        X = x;
        Y = y;
    }

    public Point(BinaryReader reader)
    {
        X = reader.ReadDouble();
        Y = reader.ReadDouble();
    }
}













