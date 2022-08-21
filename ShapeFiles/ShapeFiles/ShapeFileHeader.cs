using System.Buffers.Binary;

namespace ShapeFiles;

/// <summary>
/// https://en.wikipedia.org/wiki/Shapefile
/// Position	Field	        Value	    Type	Order
/// Byte 0	    File Code	    9994	    Integer	Big
/// Byte 4	    Unused	        0	        Integer	Big
/// Byte 8	    Unused	        0           Integer	Big
/// Byte 12	    Unused	        0           Integer	Big
/// Byte 16	    Unused	        0	        Integer	Big
/// Byte 20	    Unused	        0	        Integer	Big
/// Byte 24	    File Length	    File Length	Integer	Big
/// Byte 28	    Version	        1000	    Integer	Little
/// Byte 32	    Shape Type	    Shape Type	Integer	Little
/// Byte 36	    Bounding Box	Xmin	    Double	Little
/// Byte 44	    Bounding Box	Ymin	    Double	Little
/// Byte 52	    Bounding Box	Xmax	    Double	Little
/// Byte 60	    Bounding Box	Ymax	    Double	Little
/// Byte 68*	Bounding Box	Zmin	    Double	Little
/// Byte 76*	Bounding Box	Zmax	    Double	Little
/// Byte 84*	Bounding Box	Mmin	    Double	Little
/// Byte 92*	Bounding Box	Mmax	    Double	Little
/// * Unused, with value 0.0, if not Measured or Z type
/// </summary>
public sealed class ShapeFileHeader
{
    public static class Sections
    {
        public static readonly (Range range, Endianness endianness, Type type) FileCode = new(new Range(0, 3), Endianness.BigEndian, typeof(int));

        public static readonly (Range range, Endianness endianness, Type type) Unused = new(new Range(4, 23), Endianness.BigEndian, typeof(int));
        
        public static readonly (Range range, Endianness endianness, Type type) FileLength = new(new Range(24, 27), Endianness.BigEndian, typeof(int));

        public static readonly (Range range, Endianness endianness, Type type) Version = new(new Range(28, 31), Endianness.LittleEndian, typeof(int));

        public static readonly (Range range, Endianness endianness, Type type) ShapeType = new(new Range(32, 35), Endianness.LittleEndian, typeof(int));


        public static readonly (Range range, Endianness endianness, Type type) MBR = new(new Range(36, 67), Endianness.LittleEndian, typeof(BoundingRectangle));

        public static readonly (Range range, Endianness endianness, Type type) RangeOfZ = new(new Range(68, 83), Endianness.LittleEndian, typeof(Limits));

        public static readonly (Range range, Endianness endianness, Type type) RangeOfM = new(new Range(84, 99), Endianness.LittleEndian, typeof(Limits));
    }

    public int               FileCode   { get; private set; }
    public int               Unused     { get; private set; }
    public int               FileLength { get; private set; }
    public int               Version    { get; private set; }
    public ShapeType         ShapeType  { get; private set; }
    public BoundingRectangle MBR        { get; private set; }
    public Limits            RangeOfZ   { get; private set; }
    public Limits            RangeOfM   { get; private set; }


    public ShapeFileHeader()
    {
    }

    public void Read(BinaryReader reader)
    {
        FileCode = BinaryPrimitives.ReverseEndianness(reader.ReadInt32());

        reader.ReadBytes(Sections.Unused.range.End.Value - Sections.Unused.range.Start.Value + 1);

        FileLength = BinaryPrimitives.ReverseEndianness(reader.ReadInt32());
        Version    = reader.ReadInt32();
        ShapeType  = (ShapeType)reader.ReadInt32();

        MBR = new BoundingRectangle(reader);

        RangeOfZ = new Limits(reader);
        RangeOfM = new Limits(reader);
    }
}
