namespace ShapeFiles;

/// TriangleStrip A linked strip of triangles, where every vertex (after the first two) completes a new triangle. A new triangle is always formed by connecting the new vertex with its two immediate predecessors.
/// TriangleFan A linked fan of triangles, where every vertex (after the first two) completes a new triangle. A new triangle is always formed by connecting the new vertex with its immediate predecessor and the first vertex of the part.
/// OuterRing The outer ring of a polygon.
/// InnerRing A hole of a polygon.
/// FirstRing The first ring of a polygon of an unspecified type.
/// Ring A ring of a polygon of an unspecified type.

/// <summary>
/// Byte
/// Position  Field Value           Type Number Order
/// Byte 0 Shape                   Type 31 int 1 Little
/// Byte 4 Box                 Box double 4 Little
/// Byte 36 NumParts           NumParts int 1 Little
/// Byte 40 NumPoints          NumPoints int 1 Little
/// Byte 44 Parts              Parts int       NumParts Little
/// Byte W                     PartTypes PartTypes int NumParts Little
/// Byte                           X Points            Points Point     NumPoints Little
/// Byte Y                     Zmin Zmin           double 1 Little
/// Byte Y +8 Zmax             Zmax double 1 Little
/// Byte Y +16 Zarray          Zarray double NumPoints Little
/// Byte Z*                    Mmin Mmin     double 1 Little
/// Byte Z +8  * Mmax Mmax     double 1 Little
/// Byte Z +16 * Marray Marray double NumPoints Little
/// Note: W = 44 + (4 * NumParts), X = W + (4 * NumParts), Y = X + (16 * NumPoints),
/// Z = Y + 16 + (8 * NumPoints)
/// * optional
/// </summary>
public sealed record MultiPatch : Geometry
{
    public override ShapeType   ShapeType { get { return ShapeType.MultiPatch; } }

    public          BoundingBox Box       { get; } // Bounding Box
    public          int         NumParts  { get; } // Number of Parts
    public          int         NumPoints { get; } // Total Number of Points
    public          int[]       Parts     { get; } // Index to First Point in Part
    public          int[]       PartTypes { get; } // Part Type
    public          Point[]     Points    { get; } // Points for All Parts
    public          Limits      ZRange    { get; } // Bounding Z Range
    public          double[]    ZArray    { get; } // Z Values for All Points
    public          Limits      MRange    { get; } // Bounding Measure Range
    public          double[]    MArray    { get; } // Measures


    public MultiPatch(BoundingBox box,
                      int         numParts,
                      int         numPoints,
                      Limits      zRange,
                      Limits      mRange)
    {
        Box       = box;
        NumParts  = numParts;
        NumPoints = numPoints;
        Parts     = new int[NumParts];
        PartTypes = new int[NumParts];
        Points    = new Point[NumPoints];
        ZRange    = zRange;
        ZArray    = new double[NumPoints];
        MRange    = mRange;
        MArray    = new double[NumPoints];
    }

    public MultiPatch(BoundingBox box,
                      int         numParts,
                      int         numPoints,
                      int[]       parts,
                      int[]       partTypes,
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
        PartTypes = partTypes;
        Points    = points;
        ZRange    = zRange;
        ZArray    = zArray;
        MRange    = mRange;
        MArray    = mArray;
    }

    public MultiPatch(BinaryReader reader)
    {
        Box       = new BoundingBox(reader);
        NumParts  = reader.ReadInt32();
        NumPoints = reader.ReadInt32();
        Parts     = new int[NumParts];
        PartTypes = new int[NumParts];
        Points    = new Point[NumPoints];

        for(int i = 0; i < NumParts; i++)
        {
            Parts[i] = reader.ReadInt32();
        }

        for(int i = 0; i < NumParts; i++)
        {
            PartTypes[i] = reader.ReadInt32();
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

//The fields for a MultiPatch are
//Box The Bounding Box for the MultiPatch stored in the order Xmin, Ymin,
//Xmax, Ymax.
//NumParts The number of parts in the MultiPatch.
//NumPoints The total number of points for all parts.
//Parts An array of length NumParts. Stores, for each part, the index of its first
//point in the points array. Array indexes are with respect to 0.
//PartTypes An array of length NumParts. Stores for each part its type.
//Points An array of length NumPoints. The points for each part in the MultiPatch
//are stored end to end. The points for Part 2 follow the points for Part 1, and
//so on. The parts array holds the array index of the starting point for each
//part. There is no delimiter in the points array between parts.
//Z Range The minimum and maximum Z values for the arc stored in the order Zmin,
//Zmax.
//Z Array An array of length NumPoints. The Z values for each part in the MultiPatch
//are stored end to end. The Z values for Part 2 follow the Z values for Part 1,
//and so on. The parts array holds the array index of the starting Z value for
//each part. There is no delimiter in the Z value array between parts.
//M Range The minimum and maximum measures for the MultiPatch stored in the
//order Mmin, Mmax.
//M Array An array of length NumPoints. The measures for each part in the
//MultiPatch are stored end to end. The measures for Part 2 follow the
//measures for Part 1, and so on. The parts array holds the array index of the starting measure for each part. There is no delimiter in the measure array
//between parts.