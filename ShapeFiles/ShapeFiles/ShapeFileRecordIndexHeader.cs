using System.Buffers.Binary;

namespace ShapeFiles;

/// <summary>
/// Header for Shapefile shape index format (.shx)
/// </summary>
public sealed class ShapeFileRecordIndexHeader
{
    public static class Sections
    {
        public static readonly (Range range, Endianness endianness, Type type) Offset = new(new Range(0, 3), Endianness.BigEndian, typeof(int));

        public static readonly (Range range, Endianness endianness, Type type) Length = new(new Range(4, 7), Endianness.BigEndian, typeof(int));
    }

    public int Offset { get; private set; }
    public int Length { get; private set; }


    public ShapeFileRecordIndexHeader()
    {
    }

    public void Read(BinaryReader reader)
    {
        Offset = BinaryPrimitives.ReverseEndianness(reader.ReadInt32());
        Length = BinaryPrimitives.ReverseEndianness(reader.ReadInt32());
    }
}