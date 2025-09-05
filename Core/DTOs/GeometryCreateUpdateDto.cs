using Microsoft.AspNetCore.Http;

namespace App.Core.DTOs
{
    public class GeometryCreateUpdateDto
    {
        public string Name { get; set; } = string.Empty;

        public string Wkt { get; set; } = string.Empty;

        public string Type { get; set; } = "Point";

        public IFormFile? Photo { get; set; }

    }

}

