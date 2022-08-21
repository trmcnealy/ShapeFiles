namespace ShapeFiles;

public class Feature
{
    public long Id { get; }

    public Geometry Geometry { get; }


    public Dictionary<string, string> Attributes { get; }

}
