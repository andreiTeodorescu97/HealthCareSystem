using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class PacientGeneralMedicalDataDto
    {
        [Required(ErrorMessage = "Grupa sanguina este obligatorie!")]
        public string BloodType { get; set; }
        [Required(ErrorMessage = "Greutatea la nastere este obligatorie!")]
        public float? WeightBirth { get; set; }
        [Required(ErrorMessage = "Inaltimea la nastere este obligatorie!")]
        public float? HeightBirth { get; set; }
        [Required(ErrorMessage = "Numarul de nasteri este obligatoriu!")]
        public int? NumberOfBirths { get; set; }
        [Required(ErrorMessage = "Numarul de avorturi este obligatoriu!")]
        public int? NumberOfAvortions { get; set; }
        [Required(ErrorMessage = "Tipul de fumator este obligatoriu!")]
        public string IsSmoker { get; set; }
        public int PacientId { get; set; }
    }
}