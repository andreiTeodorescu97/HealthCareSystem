using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ConsultationDto
    {
        [Required]
        public int Height { get; set; }
        [Required]
        public int Weight { get; set; }
        public float? SystolicBp { get; set; }
        public float? DiastolicBp { get; set; }
        [Required]
        public float Temperature { get; set; }
        [Required]
        public int HeartRate { get; set; }
        public float? BloodSugar { get; set; }
        public float? BMI { get; set; }
        public int? RespiratoryRate { get; set; }
        public int? NumberOfCigarettesPerDay { get; set; }
        public string GeneralFeeling { get; set; }
        public string Comments { get; set; }
        public int AppoinmentId { get; set; }
        public DateTime DateAdded { get; set; }
        public int? PacientId { get; set; }
    }
}