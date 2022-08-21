namespace ShapeFiles;

/// <summary>
/// Byte
/// Position                        Field Value Type Number Order
/// Byte 0 Shape                   Type 18 int 1 Little
/// Byte 4 Box                 Box double 4 Little
/// Byte 36 NumPoints          NumPoints int 1 Little
/// Byte 40 Points             Points Point NumPoints Little
/// Byte X                     Zmin Zmin    double 1 Little
/// Byte X +8 Zmax             Zmax double 1 Little
/// Byte X +16 Zarray          Zarray double NumPoints Little
/// Byte Y*                    Mmin Mmin     double 1 Little
/// Byte Y +8  * Mmax Mmax     double 1 Little
/// Byte Y +16 * Marray Marray double NumPoints Little
/// Note: X = 40 + (16 * NumPoints); Y = X + 16 + (8 * NumPoints)
/// * optional
/// </summary>
public sealed record MultiPointZ : Geometry
{
    public override ShapeType ShapeType { get { return ShapeType.MultiPointZ; } }

    public BoundingBox Box       { get; } // Bounding Box
    public int         NumPoints { get; } // Number of Points
    public Point[]     Points    { get; } // The Points in the Set
    public Limits      ZRange    { get; } // Bounding Z Range
    public double[]    ZArray    { get; } // Z Values
    public Limits      MRange    { get; } // Bounding Measure Range
    public double[]    MArray    { get; } // Measures

    public MultiPointZ(BoundingBox box,
                       int         numPoints,
                       Limits      zRange,
                       Limits      mRange)
    {
        Box       = box;
        NumPoints = numPoints;
        Points    = new Point[NumPoints];
        ZRange    = zRange;
        ZArray    = new double[NumPoints];
        MRange    = mRange;
        MArray    = new double[NumPoints];
    }

    public MultiPointZ(BoundingBox box,
                       int         numPoints,
                       Point[]     points,
                       Limits      zRange,
                       double[]    zArray,
                       Limits      mRange,
                       double[]    mArray)
    {
        Box       = box;
        NumPoints = numPoints;
        Points    = points;
        ZRange    = zRange;
        ZArray    = zArray;
        MRange    = mRange;
        MArray    = mArray;
    }

    public MultiPointZ(BinaryReader reader)
    {
        Box       = new BoundingBox(reader);
        NumPoints = reader.ReadInt32();
        Points    = new Point[NumPoints];

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