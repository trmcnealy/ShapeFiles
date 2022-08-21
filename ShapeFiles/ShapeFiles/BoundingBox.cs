namespace ShapeFiles;

public readonly struct BoundingBox
{
    public Limits X { get; }
    public Limits Y { get; }
    public Limits Z { get; }

    public BoundingBox(double minX, double minY, double minZ,
                       double maxX, double maxY, double maxZ)
    {
        X = new Limits(minX,
                       maxX);
        Y = new Limits(minY,
                       maxY);
        Z = new Limits(minZ,
                       maxZ);
    }

    public BoundingBox(BinaryReader reader)
    {
        double minX = reader.ReadDouble();
        double minY = reader.ReadDouble();
        double minZ = reader.ReadDouble();
        double maxX = reader.ReadDouble();
        double maxY = reader.ReadDouble();
        double maxZ = reader.ReadDouble();

        X = new Limits(minX,
                       maxX);

        Y = new Limits(minY,
                       maxY);
        Z = new Limits(minZ,
                       maxZ);
    }
    
}

public readonly struct BoundingBoxM
{
    public Limits X { get; }
    public Limits Y { get; }
    public Limits Z { get; }
    public Limits M { get; }

    public BoundingBoxM(double minX, double minY, double minZ, double minM,
                        double maxX, double maxY, double maxZ, double maxM)
    {
        X = new Limits(minX,
                       maxX);
        Y = new Limits(minY,
                       maxY);
        Z = new Limits(minZ,
                       maxZ);
        M = new Limits(minM,
                       maxM);
    }

    public BoundingBoxM(BinaryReader reader)
    {
        double minX = reader.ReadDouble();
        double minY = reader.ReadDouble();
        double minZ = reader.ReadDouble();
        double minM = reader.ReadDouble();
        double maxX = reader.ReadDouble();
        double maxY = reader.ReadDouble();
        double maxZ = reader.ReadDouble();
        double maxM = reader.ReadDouble();

        X = new Limits(minX,
                       maxX);
        Y = new Limits(minY,
                       maxY);
        Z = new Limits(minZ,
                       maxZ);
        M = new Limits(minM,
                       maxM);
    }
    
}