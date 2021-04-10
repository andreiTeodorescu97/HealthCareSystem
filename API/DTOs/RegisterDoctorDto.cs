using System;

namespace API.DTOs
{
    public class RegisterDoctorDto
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
    }
}