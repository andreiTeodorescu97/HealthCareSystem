using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(15, MinimumLength = 6,
        ErrorMessage = "Username should be minimum 6 characters and a maximum of 15 characters")]
        public string Username { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 6,
        ErrorMessage = "Password should be minimum 6 characters and a maximum of 15 characters")]
        public string Password { get; set; }
        public bool IsPacientAccount { get; set; }
        public PacientDTO pacientDTO { get; set; }
        public DoctorDto doctorDTO { get; set; }
    }
}