namespace ShapeFiles;

public readonly struct Limits
{
    public double Min { get; }
    public double Max { get; }

    public Limits(double min,
                  double max)
    {
        Min = min;
        Max = max;
    }

    public Limits(BinaryReader reader)
    {
        Min = reader.ReadDouble();
        Max = reader.ReadDouble();
    }
}


public readonly struct Pair<A, B>
    where A : notnull
    where B : notnull
{
    
    public A First { get; }
    
    public B Second { get; }
    
    public Pair(A first, B second)
    {
        First  = first;
        Second = second;
    }

    public bool Equals(Pair<A, B> other)
    {
        return EqualityComparer<A>.Default.Equals(First, other.First) && EqualityComparer<B>.Default.Equals(Second, other.Second);
    }

    public override bool Equals(object? obj)
    {
        return obj is Pair<A, B> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(First, Second);
    }

    public static bool operator ==(Pair<A, B> left,
                                   Pair<A, B> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Pair<A, B> left,
                                   Pair<A, B> right)
    {
        return !left.Equals(right);
    }
}


