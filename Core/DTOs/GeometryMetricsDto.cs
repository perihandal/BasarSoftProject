using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.DTOs
{
    public class GeometryMetricsDto
    {
        public int GeometryId { get; set; }

        public double? Area { get; set; }
        public double? Length { get; set; }

        // Geometri tiplerine göre
        public Point Centroid { get; set; }
        public Polygon BoundingBox { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
    }
}
