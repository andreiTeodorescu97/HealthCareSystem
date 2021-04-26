namespace API.DTOs
{
    public class UpdateAppoinmentStatusDto
    {
        public int AppoinmentId { get; set; }
        public int NewStatusId { get; set; }
    }
}