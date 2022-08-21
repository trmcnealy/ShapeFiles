namespace ShapeFiles;

/// <summary>
/// Byte
/// Position                        Field Value Type Number Order
/// Byte 0 Shape                   Type 28 int 1 Little
/// Byte 4 Box                 Box double 4 Little
/// Byte 36 NumPoints          NumPoints int 1 Little
/// Byte 40 Points             Points Point NumPoints Little
/// Byte X*                    Mmin Mmin    double 1 Little
/// Byte X +8  * Mmax Mmax     double 1 Little
/// Byte X +16 * Marray Marray double NumPoints Little
/// Note: X = 40 + (16 * NumPoints)
/// * optional
/// </summary>
public sealed record MultiPointM : Geometry
{
    public override ShapeType   ShapeType { get { return ShapeType.MultiPointM; } }
    
    public          BoundingBox Box       { get; } // Bounding Box
    public          int         NumPoints { get; } // Number of Points
    public          Point[]     Points    { get; } // The Points in the Set
    public          Limits      MRange    { get; } // Bounding Measure Range
    public          double[]    MArray    { get; } // Measures

    public MultiPointM(BoundingBox box,
                       int         numPoints,
                       Limits      mRange)
    {
        Box       = box;
        NumPoints = numPoints;
        Points    = new Point[NumPoints];
        MRange    = mRange;
        MArray    = new double[NumPoints];
    }

    public MultiPointM(BoundingBox box,
                       int         numPoints,
                       Point[]     points,
                       Limits      mRange,
                       double[]    mArray)
    {
        Box       = box;
        NumPoints = numPoints;
        Points    = points;
        MRange    = mRange;
        MArray    = mArray;
    }

    public MultiPointM(BinaryReader reader)
    {
        Box       = new BoundingBox(reader);
        NumPoints = reader.ReadInt32();
        Points    = new Point[NumPoints];

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

//The fields for a MultiPointM are
//Box The Bounding Box for the MultiPointM stored in the order Xmin, Ymin,
//Xmax, Ymax
//NumPoints The number of Points
//Points An array of Points of length NumPoints
//M Range The minimum and maximum measures for the MultiPointM stored in the
//order Mmin, Mmax
//M Array An array of measures of length NumPoints
