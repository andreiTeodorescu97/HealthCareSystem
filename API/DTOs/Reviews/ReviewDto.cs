using System;
using API.Entities;

namespace API.DTOs.Reviews
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
        public int PacientId { get; set; }
        public string PacientFirstName { get; set; }
        public string PacientSecondName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorSecondName { get; set; }
        public string DoctorProfilePicture { get; set; }
    }
}