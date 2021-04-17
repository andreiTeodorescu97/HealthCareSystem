namespace API.DTOs
{
    public class FreeHourDto
    {
        public int Id { get; set; }
        public string FromHour { get; set; }
        public string ToHour { get; set; }
        public int FromTimeSpan {get;set;}
        public int ToTimeSpan {get;set;}
    }
}