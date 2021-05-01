using System;

namespace API.DTOs
{
    public class GetPacientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string IdentityNumber { get; set; }
        public string Series { get; set; }
        public string CNP { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public int UserId { get; set; }
        public PacientContactDto PacientContact { get; set; }
        public PacientGeneralMedicalDataDto PacientGeneralMedicalData { get; set; }
    }
}