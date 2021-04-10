using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterPacientDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string IdentityNumber { get; set; }
        [Required]
        public string Series { get; set; }
        [Required]
        public string CNP { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
    }
}