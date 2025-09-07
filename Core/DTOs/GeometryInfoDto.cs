namespace App.Core.DTOs
{
    public class GeometryInfoDto
    {
        public string FullAddress { get; set; }
        public string Phone { get; set; }
        public string PhotoBase64 { get; set; }
        public string Description { get; set; }
        public string OpeningHours { get; set; }   
        public int GeometryId { get; set; }        // Hangi Geometry’ye ait olduğu
    }
}
