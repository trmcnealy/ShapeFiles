using System.IO;
using System.Text;

namespace ShapeFiles;



public sealed class ShapeFileReader
{

    public ShapeFileType FileType { get; }

    public BinaryReader Reader { get; }


    public static ShapeFileType GetFileType(string file)
    {
        switch(Path.GetExtension(file))
        {
            case ".shp":
            {
                return ShapeFileType.Shp;
            }
            case ".shx":
            {
                return ShapeFileType.Shx;
            }
            case ".dbf":
            {
                return ShapeFileType.Dbf;
            }
            case ".sbn":
            {
                return ShapeFileType.Sbn;
            }
            default:
            {
                throw new ArgumentOutOfRangeException(nameof(file), file, null);
            }
        }
    }

    public ShapeFileReader(string file)
        :this(new FileStream(file, FileMode.Open, FileAccess.Read), GetFileType(file))
    {}

    public ShapeFileReader(string file, Encoding encoding)
        :this(new FileStream(file, FileMode.Open, FileAccess.Read), GetFileType(file), encoding)
    {}

    public ShapeFileReader(string file, Encoding encoding, bool leaveOpen)
        :this(new FileStream(file, FileMode.Open, FileAccess.Read), GetFileType(file), encoding, leaveOpen)
    {}

    public ShapeFileReader(Stream input, ShapeFileType fileType)
    {
        Reader = new BinaryReader(input);
        FileType = fileType;
    }

    public ShapeFileReader(Stream input, ShapeFileType fileType, Encoding encoding)
    {
        Reader   = new BinaryReader(input, encoding);
        FileType = fileType;
    }

    public ShapeFileReader(Stream input, ShapeFileType fileType, Encoding encoding, bool leaveOpen)
    {
        Reader   = new BinaryReader(input, encoding, leaveOpen);
        FileType = fileType;
    }

    
    public List<Geometry> Read()
    {
        List<Geometry> geometries;
        
        ShapeFileHeader header = new();
        header.Read(Reader);
        
        if(FileType == ShapeFileType.Shp)
        {
            geometries = new List<Geometry>(100);

            ShapeFileRecordHeader recordHeader = new();
            
            
            Geometry        geometry;
            ShapeType       shapeType;

            while(Reader.BaseStream.Position < Reader.BaseStream.Length)
            {
                recordHeader.Read(Reader);

                shapeType = (ShapeType)Reader.ReadInt32();

                geometry = GetGeometryFrom(shapeType, Reader);

                Console.WriteLine(Enum.GetName<ShapeType>(geometry.ShapeType));

                geometries.Add(geometry);
            }

            return geometries;
        }
        //Shx Offsets point to Shp file
        //else if(FileType == ShapeFileType.Shx)
        //{
        //    ShapeFileRecordIndexHeader recordHeader = new();
        //    recordHeader.Read(Reader);
        //
        //    geometries = new List<Geometry>(recordHeader.Number);
        //
        //    for(int i = 0; i < recordHeader.Number; i++)
        //    {
        //        geometries.Add(GetGeometryFrom(header.ShapeType, Reader));
        //    }
        //}
        
        throw new NotSupportedException("Only shp supported at this time.");
    }

    public static Geometry GetGeometryFrom(ShapeType shapeType, BinaryReader Reader)
    {
        switch(shapeType)
        {
            case ShapeType.Null:
            {
                return new Null();
            }
            case ShapeType.Point:
            {
                return new Point(Reader);
            }
            case ShapeType.PolyLine:
            {
                return new PolyLine(Reader);
            }
            case ShapeType.Polygon:
            {
                return new Polygon(Reader);
            }
            case ShapeType.MultiPoint:
            {
                return new MultiPoint(Reader);
            }
            case ShapeType.PointZ:
            {
                return new PointZ(Reader);
            }
            case ShapeType.PolyLineZ:
            {
                return new PolyLineZ(Reader);
            }
            case ShapeType.PolygonZ:
            {
                return new PolygonZ(Reader);
            }
            case ShapeType.MultiPointZ:
            {
                return new MultiPointZ(Reader);
            }
            case ShapeType.PointM:
            {
                return new PointM(Reader);
            }
            case ShapeType.PolyLineM:
            {
                return new PolyLineM(Reader);
            }
            case ShapeType.PolygonM:
            {
                return new PolygonM(Reader);
            }
            case ShapeType.MultiPointM:
            {
                return new MultiPointM(Reader);
            }
            case ShapeType.MultiPatch:
            {
                return new MultiPatch(Reader);
            }
            default:
            {
                throw new ArgumentOutOfRangeException(nameof(shapeType), shapeType, null);
            }
        }
    }

}



