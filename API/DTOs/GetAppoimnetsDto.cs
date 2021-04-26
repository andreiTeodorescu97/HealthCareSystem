using System;

namespace API.DTOs
{
    public class GetAppoimnetsDto
    {
        public int Id { get; set; }
        public string AppoinmentDate { get; set; }
        public string AppoinmentHour { get; set; }
        public int AppoinmentStartSpan { get; set; }
        public int AppoinmentEndSpan { get; set; }
        public string Reason { get; set; }
        public int DateId { get; set; }
        public DateTime DateCreated { get; set; }
        public int PacientId { get; set; }
        public string PacientFirstName { get; set; }
        public string PacientSecondName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorSecondName { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public bool IsConsultationAdded { get; set; }
    }
}