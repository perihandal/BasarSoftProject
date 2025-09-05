using NetTopologySuite.Geometries;

namespace App.Core.DTOs
{
    public class GeometryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Wkt { get; set; }
        public string Type { get; set; }

        // GeometryInfo
        public string FullAddress { get; set; }
        public string Phone { get; set; }
        public string PhotoBase64 { get; set; }
        public string Description { get; set; }
        public string OpeningHours { get; set; }

        public double? Area { get; set; }
        public double? Length { get; set; }
        public Point? Centroid { get; set; }
        public Polygon? BoundingBox { get; set; }
        public Point? StartPoint { get; set; }
        public Point? EndPoint { get; set; }

    }

}

