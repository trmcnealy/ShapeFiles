

using System.Data;
using ShapeFiles;

ShapeFileReader reader = new ShapeFileReader(@"D:\TFS_Sources\EngineeringTools\Utilities\ShapeFiles\ShapeFiles\ShapeFiles\Resources\USA_Cities_Towns.shp");
List<Geometry> geometries = reader.Read();

Console.WriteLine(geometries.Count);



DataTable dt = DbfFileReader.ReadDBF(@"D:\TFS_Sources\EngineeringTools\Utilities\ShapeFiles\ShapeFiles\ShapeFiles\Resources\USA_Cities_Towns.dbf");


foreach(object? dataColumn in dt.Columns)
{
    Console.Write(dataColumn);
}
Console.WriteLine();

foreach(DataRow dataRow in dt.Rows)
{
    foreach(object? item in dataRow.ItemArray)
    {
        Console.Write(item);
    }
    Console.WriteLine();
}


#if DEBUG
Console.WriteLine("press any key to exit.");
Console.Read();
#endif