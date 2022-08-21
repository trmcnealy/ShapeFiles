using System.Buffers.Binary;

namespace ShapeFiles;

/// <summary>
/// Header for Shapefile shape format (.shp)
/// </summary>
public sealed class ShapeFileRecordHeader
{
    public static class Sections
    {
        public static readonly (Range range, Endianness endianness, Type type) Number = new(new Range(0, 3), Endianness.BigEndian, typeof(int));

        public static readonly (Range range, Endianness endianness, Type type) Length = new(new Range(4, 7), Endianness.BigEndian, typeof(int));
    }

    public int Number { get; private set; }
    public int Length { get; private set; }


    public ShapeFileRecordHeader()
    {
    }

    public void Read(BinaryReader reader)
    {
        Number = BinaryPrimitives.ReverseEndianness(reader.ReadInt32());
        Length = BinaryPrimitives.ReverseEndianness(reader.ReadInt32());
    }
}