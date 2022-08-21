namespace ShapeFiles;

/// <summary>
/// Byte
/// Position               Field Value Type Number Order
/// Byte 0 Shape          Type 3 int 1 Little
/// Byte 4 Box        Box double 4 Little
/// Byte 36 NumParts  NumParts int 1 Little
/// Byte 40 NumPoints NumPoints int 1 Little
/// Byte 44 Parts     Parts int NumParts Little
/// Byte X            Points Points Point NumPoints Little
/// Note: X = 44 + 4 * NumParts
/// </summary>
public sealed record PolyLine : Geometry
{
    public override ShapeType ShapeType { get { return ShapeType.PolyLine; } }

    public BoundingBox Box       { get; } // Bounding Box
    public int         NumParts  { get; } // Number of Parts
    public int         NumPoints { get; } // Total Number of Points
    public int[]       Parts     { get; } // Index to First Point in Part
    public Point[]     Points    { get; } // Points for All Parts

    public PolyLine(BoundingBox box,
                    int         numParts,
                    int         numPoints)
    {
        Box       = box;
        NumParts  = numParts;
        NumPoints = numPoints;
        Parts     = new int[NumParts];
        Points    = new Point[NumPoints];
    }

    public PolyLine(BoundingBox box,
                    int         numParts,
                    int         numPoints,
                    int[]       parts,
                    Point[]     points)
    {
        Box       = box;
        NumParts  = numParts;
        NumPoints = numPoints;
        Parts     = parts;
        Points    = points;
    }

    public PolyLine(BinaryReader reader)
    {
        Box       = new BoundingBox(reader);
        NumParts  = reader.ReadInt32();
        NumPoints = reader.ReadInt32();
        Parts     = new int[NumParts];
        Points    = new Point[NumPoints];

        for(int i = 0; i < NumParts; i++)
        {
            Parts[i] = reader.ReadInt32();
        }

        for(int i = 0; i < NumPoints; i++)
        {
            Points[i] = new Point(reader);
        }
    }
}

//The fields for a PolyLine are described in detail below:
//Box The Bounding Box for the PolyLine stored in the order Xmin, Ymin, Xmax,
//Ymax.
//NumParts The number of parts in the PolyLine.
//NumPoints The total number of points for all parts.
//Parts An array of length NumParts. Stores, for each PolyLine, the index of its
//first point in the points array. Array indexes are with respect to 0.
//Points An array of length NumPoints. The points for each part in the PolyLine are
//stored end to end. The points for Part 2 follow the points for Part 1, and so
//on. The parts array holds the array index of the starting point for each part.
//There is no delimiter in the points array between parts.