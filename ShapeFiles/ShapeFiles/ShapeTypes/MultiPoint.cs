namespace ShapeFiles;

/// <summary>
/// Byte
/// Position               Field Value Type Number Order
/// Byte 0 Shape          Type 8 int 1 Little
/// Byte 4 Box        Box double 4 Little
/// Byte 36 NumPoints NumPoints int 1 Little
/// Byte 40 Points    Points Point NumPoints Little
/// </summary>
public sealed record MultiPoint : Geometry
{
    public override ShapeType   ShapeType { get { return ShapeType.MultiPoint; } }

    public          BoundingBox Box       { get; } // Bounding Box
    public          int         NumPoints { get; } // Number of Points
    public          Point[]     Points    { get; } // The Points in the Set

    public MultiPoint(BoundingBox box,
                      int         numPoints)
    {
        Box       = box;
        NumPoints = numPoints;
        Points    = new Point[NumPoints];
    }

    public MultiPoint(BoundingBox box,
                      int         numPoints,
                      Point[]     points)
    {
        Box       = box;
        NumPoints = numPoints;
        Points    = points;
    }

    public MultiPoint(BinaryReader reader)
    {
        Box       = new BoundingBox(reader);
        NumPoints = reader.ReadInt32();
        Points    = new Point[NumPoints];

        for(int i = 0; i < NumPoints; i++)
        {
            Points[i] = new Point(reader);
        }
    }
}