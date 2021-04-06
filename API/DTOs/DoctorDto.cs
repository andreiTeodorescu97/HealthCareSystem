using System;

namespace API.DTOs
{
    public class DoctorDto
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}