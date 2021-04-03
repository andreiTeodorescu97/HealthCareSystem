namespace API.DTOs
{
    public class PacientContactDto
    {
        public string Street { get; set; }
        public int? StreetNumber { get; set; }
        public string FirstPhone { get; set; }
        public string SecondPhone { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
    }
}