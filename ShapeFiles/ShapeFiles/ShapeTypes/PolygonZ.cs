namespace ShapeFiles;

/// <summary>
/// Byte
/// Position  Field Value           Type Number Order
/// Byte 0 Shape                   Type 15 int 1 Little
/// Byte 4 Box                 Box double 4 Little
/// Byte 36 NumParts           NumParts int 1 Little
/// Byte 40 NumPoints          NumPoints int 1 Little
/// Byte 44 Parts              Parts int NumParts Little
/// Byte X                     Points Points Point NumPoints Little
/// Byte                           Y Zmin        Zmin double 1 Little
/// Byte Y +8 Zmax             Zmax double 1 Little
/// Byte Y +16 Zarray          Zarray double NumPoints Little
/// Byte Z*                    Mmin Mmin     double 1 Little
/// Byte Z +8  * Mmax Mmax     double 1 Little
/// Byte Z +16 * Marray Marray double NumPoints Little
/// Note: X = 44 + (4 * NumParts), Y = X + (16 * NumPoints), Z = Y + 16 + (8 *NumPoints)
/// * optional
/// </summary>
public sealed record PolygonZ : Geometry
{
    public override ShapeType ShapeType { get { return ShapeType.PolygonZ; } }

    public BoundingBox Box       { get; } // Bounding Box
    public int         NumParts  { get; } // Number of Parts
    public int         NumPoints { get; } // Total Number of Points
    public int[]       Parts     { get; } // Index to First Point in Part
    public Point[]     Points    { get; } // Points for All Parts
    public Limits      ZRange    { get; } // Bounding Z Range
    public double[]    ZArray    { get; } // Z Values for All Points
    public Limits      MRange    { get; } // Bounding Measure Range
    public double[]    MArray    { get; } // Measures

    public PolygonZ(BoundingBox box,
                    int         numParts,
                    int         numPoints,
                    Limits      zRange,
                    Limits      mRange)
    {
        Box       = box;
        NumParts  = numParts;
        NumPoints = numPoints;
        Parts     = new int[NumParts];
        Points    = new Point[NumPoints];
        ZRange    = zRange;
        ZArray    = new double[NumPoints];
        MRange    = mRange;
        MArray    = new double[NumPoints];
    }

    public PolygonZ(BoundingBox box,
                    int         numParts,
                    int         numPoints,
                    int[]       parts,
                    Point[]     points,
                    Limits      zRange,
                    double[]    zArray,
                    Limits      mRange,
                    double[]    mArray)
    {
        Box       = box;
        NumParts  = numParts;
        NumPoints = numPoints;
        Parts     = parts;
        Points    = points;
        ZRange    = zRange;
        ZArray    = zArray;
        MRange    = mRange;
        MArray    = mArray;
    }

    public PolygonZ(BinaryReader reader)
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

        ZRange = new Limits(reader);
        ZArray = new double[NumPoints];

        for(int i = 0; i < NumPoints; i++)
        {
            ZArray[i] = reader.ReadDouble();
        }

        MRange = new Limits(reader);
        MArray = new double[NumPoints];

        for(int i = 0; i < NumPoints; i++)
        {
            MArray[i] = reader.ReadDouble();
        }
    }
}

//    The fields for a PolygonZ are
//Box The Bounding Box for the PolygonZ stored in the order Xmin, Ymin,
//Xmax, Ymax.
//NumParts The number of rings in the PolygonZ.
//NumPoints The total number of points for all rings.
//Parts An array of length NumParts. Stores, for each ring, the index of its first
//point in the points array. Array indexes are with respect to 0.
//Points An array of length NumPoints. The points for each ring in the PolygonZ are
//stored end to end. The points for Ring 2 follow the points for Ring 1, and so
//on. The parts array holds the array index of the starting point for each ring.
//There is no delimiter in the points array between rings.
//Z Range The minimum and maximum Z values for the arc stored in the order Zmin,
//Zmax.
//Z Array An array of length NumPoints. The Z values for each ring in the PolygonZ
//are stored end to end. The Z values for Ring 2 follow the Z values for
//Ring 1, and so on. The parts array holds the array index of the starting Z
//value for each ring. There is no delimiter in the Z value array between
//rings.
//M Range The minimum and maximum measures for the PolygonZ stored in the order
//Mmin, Mmax.
//M Array An array of length NumPoints. The measures for each ring in the PolygonZ
//are stored end to end. The measures for Ring 2 follow the measures for
//Ring 1, and so on. The parts array holds the array index of the starting
//measure for each ring. There is no delimiter in the measure array between
//rings.