using System;

namespace API.Entities
{
    public class PacientHistory
    {
        public int Id { get; set; }
        public int PacientId { get; set; }
        public Pacient Pacient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int TotalNumberOfVisits { get; set; }
        public DateTime LastVisitDate { get; set; }
    }
}