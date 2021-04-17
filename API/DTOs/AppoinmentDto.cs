namespace API.DTOs
{
    public class AppoinmentDto
    {
        public int Id { get; set; }
        public string AppoinmentDate { get; set; }
        public string AppoinmentHour { get; set; }
        public string Reason { get; set; }
    }
}