using System;

namespace API.Entities
{
    public class Appoinment
    {
        public int Id { get; set; }
        public string AppoinmentDate { get; set; }
        public string AppoinmentHour { get; set; }
        public int AppoinmentStartSpan { get; set; }
        public int AppoinmentEndSpan { get; set; }
        public string Reason { get; set; }
        public int DateId { get; set; }
        public bool IsConsultationAdded { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public int PacientId { get; set; }
        public Pacient Pacient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public Consultation Consultation { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}