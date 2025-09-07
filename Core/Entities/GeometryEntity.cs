using NetTopologySuite.Geometries;

namespace App.Core.Entities
{
    public enum GeometryType
    {
        Point = 1,
        LineString = 2,
        Polygon = 3
    }
    public class GeometryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public string Wkt { get; set; } = default!;
        public GeometryType Type { get; set; }

        
        public Geometry? Geoloc { get; set; }

        public GeometryInfoEntity GeometryInfo { get; set; }
        public GeometryMetricsEntity GeometryMetrics { get; set; }
    }
}
