namespace API.DTOs
{
    public class MakeAnAppoinmentDto
    {
        public int DoctorId { get; set; }
        public long DayUnixTime { get; set; }
        public int FromTimeSpan { get; set; }
        public int ToTimeSpan { get; set; }
        public string Reason { get; set; }
    }
}