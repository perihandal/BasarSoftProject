using System;

namespace App.Core.Entities
{
    public class GeometryInfoEntity
    {
        public int Id { get; set; }

        public string FullAddress { get; set; }
        public string Phone { get; set; }
        public string PhotoBase64 { get; set; }
        public string Description { get; set; }
        public string OpeningHours { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        public int GeometryId { get; set; } // FK
       
        public GeometryEntity GeometryEntity { get; set; }
    }
}
