namespace ShapeFiles;

public abstract record Geometry
{
    public abstract ShapeType ShapeType { get; }
}


public sealed record Null : Geometry
{
    public override ShapeType ShapeType { get { return ShapeType.Null; } }

    public Null()
    {
    }
}