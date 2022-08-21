namespace ShapeFiles;

//public sealed class ShapeFileRecord
//{
//    public static class Sections
//    {
//        public static readonly (Range range, Endianness endianness, Type type) Number = new(new Range(0, 3), Endianness.LittleEndian, typeof(ShapeType));

//        public static readonly (Range range, Endianness endianness, Type type) Length = new(new Range(4, int.MaxValue), Endianness.LittleEndian, typeof(int));
//    }

//    public ShapeType ShapeType { get; private set; }
//    public byte[]?   Content   { get; private set; }


//    public ShapeFileRecord()
//    {
//        Content = Array.Empty<byte>();
//    }

//    public void Read(BinaryReader reader)//, ShapeFileRecordHeader recordHeader)
//    {
//        ShapeType = (ShapeType)reader.ReadInt32();
//        //Content   = reader.ReadBytes(recordHeader.Length);
//    }

//    public void Read(BinaryReader reader, ShapeFileRecordIndexHeader recordIndexHeader)
//    {
//        reader.BaseStream.Position = recordIndexHeader.Offset;

//        ShapeType = (ShapeType)reader.ReadInt32();
//        Content   = reader.ReadBytes(recordIndexHeader.Length);
//    }
//}