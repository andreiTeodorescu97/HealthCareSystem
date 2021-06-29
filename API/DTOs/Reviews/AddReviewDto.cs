namespace API.DTOs.Reviews
{
    public class AddReviewDto
    {
        public int Rating { get; set; }
        public string Content { get; set; }
        public int DoctorId { get; set; }
        public int? PacientId { get; set; }
    }
}