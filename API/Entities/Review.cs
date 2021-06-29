using System;

namespace API.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
        public int PacientId { get; set; }
        public Pacient Pacient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}