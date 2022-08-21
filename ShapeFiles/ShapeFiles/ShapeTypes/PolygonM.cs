namespace ShapeFiles;

/// <summary>
/// Byte
/// Position  Field Value            Type Number Order
/// Byte 0 Shape                    Type 25 int 1 Little
/// Byte 4 Box                  Box double 4 Little
/// Byte 36 NumParts            NumParts int 1 Little
/// Byte 40 NumPoints           NumPoints int 1 Little
/// Byte 44 Parts               Parts int NumParts Little
/// Byte X                      Points Points Point NumPoints Little
/// Byte Y*                         Mmin Mmin     double 1 Little
/// Byte Y + 8  * Mmax Mmax     double 1 Little
/// Byte Y + 16 * Marray Marray double NumPoints Little
/// Note: X = 44 + (4 * NumParts), Y = X + (16 * NumPoints)
/// * optional
/// </summary>
public sealed record PolygonM : Geometry
{
    public override ShapeType ShapeType { get { return ShapeType.PolygonM; } }

    public BoundingBox Box       { get; } // Bounding Box
    public int         NumParts  { get; } // Number of Parts
    public int         NumPoints { get; } // Total Number of Points
    public int[]       Parts     { get; } // Index to First Point in Part
    public Point[]     Points    { get; } // Points for All Parts
    public Limits      MRange    { get; } // Bounding Measure Range
    public double[]    MArray    { get; } // Measures for All Points


    public PolygonM(BoundingBox box,
                    int         numParts,
                    int         numPoints,
                    Limits      mRange)
    {
        Box       = box;
        NumParts  = numParts;
        NumPoints = numPoints;
        Parts     = new int[NumParts];
        MRange    = mRange;
        Points    = new Point[NumPoints];
        MArray    = new double[NumPoints];
    }

    public PolygonM(BoundingBox box,
                    int         numParts,
                    int         numPoints,
                    int[]       parts,
                    Point[]     points,
                    Limits      mRange,
                    double[]    mArray)
    {
        Box       = box;
        NumParts  = numParts;
        NumPoints = numPoints;
        Parts     = parts;
        Points    = points;
        MRange    = mRange;
        MArray    = mArray;
    }

    public PolygonM(BinaryReader reader)
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

        MRange = new Limits(reader);
        MArray = new double[NumPoints];

        for(int i = 0; i < NumPoints; i++)
        {
            MArray[i] = reader.ReadDouble();
        }
    }
}

//    The fields for a PolygonM are
//Box The Bounding Box for the PolygonM stored in the order Xmin, Ymin,
//Xmax, Ymax.
//NumParts The number of rings in the PolygonM.
//NumPoints The total number of points for all rings.
//Parts An array of length NumParts. Stores, for each ring, the index of its first
//point in the points array. Array indexes are with respect to 0.
//Points An array of length NumPoints. The points for each ring in the PolygonM
//are stored end to end. The points for Ring 2 follow the points for Ring 1,
//and so on. The parts array holds the array index of the starting point for
//each ring. There is no delimiter in the points array between rings.
//M Range The minimum and maximum measures for the PolygonM stored in the order
//Mmin, Mmax.
//M Array An array of length NumPoints. The measures for each ring in the PolygonM
//are stored end to end. The measures for Ring 2 follow the measures for
//Ring 1, and so on. The parts array holds the array index of the starting
//measure for each ring. There is no delimiter in the measure array between
//rings.

//The Bounding Box is stored in the order Xmin, Ymin, Xmax, Ymax.
//The bounding Z Range is stored in the order Zmin, Zmax. Bounding M Range is stored
//in the order Mmin, Mmax.