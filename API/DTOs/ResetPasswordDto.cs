using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ResetPasswordDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 6,
        ErrorMessage = "Parola trebuie sa aibe o lungine intre 6 si 15 caractere! Trebuie sa contina cel putin o litera mica, o litera mare si o cifra!")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$", 
        ErrorMessage = "Parola are formatul invalid! Trebuie sa contina cel putin o litera mica, o litera mare, o cifra si un caracter special!")]
        public string Password { get; set; }
    }
}