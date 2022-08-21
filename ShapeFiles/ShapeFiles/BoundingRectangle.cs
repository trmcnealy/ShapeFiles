namespace ShapeFiles;

public readonly struct BoundingRectangle
{
    public Limits X { get; }
    public Limits Y { get; }

    public BoundingRectangle(double minX,
                                    double minY,
                                    double maxX,
                                    double maxY)
    {
        X = new Limits(minX,
                       maxX);
        Y = new Limits(minY,
                       maxY);
    }

    public BoundingRectangle(BinaryReader reader)
    {
        double minX = reader.ReadDouble();
        double minY = reader.ReadDouble();
        double maxX = reader.ReadDouble();
        double maxY = reader.ReadDouble();

        X = new Limits(minX,
                       maxX);

        Y = new Limits(minY,
                       maxY);
    }
}
