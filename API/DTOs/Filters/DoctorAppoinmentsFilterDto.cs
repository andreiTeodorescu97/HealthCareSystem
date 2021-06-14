namespace API.DTOs.Filters
{
    public class DoctorAppoinmentsFilterDto
    {
        public int DateFrom { get; set; }
        public int DateTo { get; set; }
        public string PacientFirstName { get; set; }
        public string PacientSecondName { get; set; }
        public int StatusId { get; set; }
    }
}