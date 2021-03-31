using System;

namespace API.DTOs
{
    public class PacientDTO
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string IdentityNumber { get; set; }
        public string Series { get; set; }
        public string CNP { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}