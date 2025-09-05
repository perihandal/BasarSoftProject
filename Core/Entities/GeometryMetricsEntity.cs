using NetTopologySuite.Geometries;

namespace App.Core.Entities
{
    public class GeometryMetricsEntity
    {
        public int GeometryId { get; set; } // PK & FK

        public double? Area { get; set; }       // Polygon
        public double? Length { get; set; }     // LineString
        public Point? Centroid { get; set; }
        public Polygon? BoundingBox { get; set; }
        public Point? StartPoint { get; set; }
        public Point? EndPoint { get; set; }

        // Navigasyon property
        public GeometryEntity GeometryEntity { get; set; }
    }
}
