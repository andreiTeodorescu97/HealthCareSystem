namespace API.Entities
{
    public class PacientContact
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string FirstPhone {get;set;}
        public string SecondPhone { get; set; }
        public int PacientId {get;set;}
        public Pacient Pacient { get; set; }
        public int CityId {get;set;}
        public City City { get; set; }
    }
}