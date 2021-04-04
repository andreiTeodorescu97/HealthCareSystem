namespace API.DTOs
{
    public class PacientContactDto
    {
        public string Street { get; set; }
        public int? StreetNumber { get; set; }
        public string FirstPhone { get; set; }
        public string SecondPhone { get; set; }
        public int? CityId { get; set; }
        public int? RegionId { get; set; }
    }
}