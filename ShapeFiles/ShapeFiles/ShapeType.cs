namespace ShapeFiles;

public enum ShapeType
{
    /// <summary>
    /// None
    /// </summary>
    Null        = 0,
    /// <summary>
    /// X, Y
    /// </summary>
    Point       = 1,
    /// <summary>
    /// MBR, Number of parts, Number of points, Parts, Points
    /// </summary>
    PolyLine = 3,
    /// <summary>
    /// MBR, Number of parts, Number of points, Parts, Points
    /// </summary>
    Polygon     = 5,
    /// <summary>
    /// MBR, Number of points, Points
    /// Optional: M
    /// </summary>
    MultiPoint  = 8,
    /// <summary>
    /// X, Y, Z
    /// </summary>
    PointZ      = 11,
    /// <summary>
    /// Mandatory: MBR, Number of parts, Number of points, Parts, Points, Z range, Z array
    /// Optional: M range, M array
    /// </summary>
    PolyLineZ   = 13,
    /// <summary>
    /// Mandatory: MBR, Number of parts, Number of points, Parts, Points, Z range, Z array
    /// Optional: M range, M array
    /// </summary>
    PolygonZ    = 15,
    /// <summary>
    /// Mandatory: MBR, Number of points, Points, Z range, Z array
    /// Optional: M range, M array
    /// </summary>
    MultiPointZ = 18,
    /// <summary>
    /// X, Y, M
    /// </summary>
    PointM      = 21,
    /// <summary>
    /// Mandatory: MBR, Number of parts, Number of points, Parts, Points
    /// Optional: M range, M array
    /// </summary>
    PolyLineM   = 23,
    /// <summary>
    /// Mandatory: MBR, Number of parts, Number of points, Parts, Points
    /// Optional: M range, M array
    /// </summary>
    PolygonM    = 25,
    /// <summary>
    /// Mandatory: MBR, Number of points, Points
    /// Optional Fields: M range, M array
    /// </summary>
    MultiPointM = 28,
    /// <summary>
    /// Mandatory: MBR, Number of parts, Number of points, Parts, Part types, Points, Z range, Z array
    /// Optional: M range, M array
    /// </summary>
    MultiPatch  = 31
}

public enum MultiPatchTypes
{
    TriangleStrip = 0,
    TriangleFan   = 1,
    OuterRing     = 2,
    InnerRing     = 3,
    FirstRing     = 4,
    Ring          = 5
}

/// <summary>
/// Mandatory files
/// .shp — shape format; the feature geometry itself {content-type: x-gis/x-shapefile}
/// .shx — shape index format; a positional index of the feature geometry to allow seeking forwards and backwards quickly {content-type: x-gis/x-shapefile}
/// .dbf — attribute format; columnar attributes for each shape, in dBase IV format {content-type: application/octet-stream OR text/plain}
/// Other files
/// .prj — projection description, using a well-known text representation of coordinate reference systems {content-type: text/plain OR application/text}
/// .sbn and .sbx — a spatial index of the features {content-type: x-gis/x-shapefile}
/// .fbn and .fbx — a spatial index of the features that are read-only {content-type: x-gis/x-shapefile}
/// .ain and .aih — an attribute index of the active fields in a table {content-type: x-gis/x-shapefile}
/// .ixs — a geocoding index for read-write datasets {content-type: x-gis/x-shapefile}
/// .mxs — a geocoding index for read-write datasets (ODB format) {content-type: x-gis/x-shapefile}
/// .atx — an attribute index for the .dbf file in the form of shapefile.columnname.atx (ArcGIS 8 and later) {content-type: x-gis/x-shapefile }
/// .shp.xml — geospatial metadata in XML format, such as ISO 19115 or other XML schema {content-type: application/fgdc+xml}
/// .cpg — used to specify the code page (only for .dbf) for identifying the character encoding to be used {content-type: text/plain OR x-gis/x-shapefile }
/// .qix — an alternative quadtree spatial index used by MapServer and GDAL/OGR software {content-type: x-gis/x-shapefile}
/// </summary>
public enum ShapeFileType
{
    Shp,
    Shx,
    Dbf,
    Sbn
}