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
        public int? SystolicBp { get; set; }
        public int? DiastolicBp { get; set; }
        [Required]
        public int Temperature { get; set; }
        [Required]
        public int HeartRate { get; set; }
        public int? BloodSugar { get; set; }
        public int? BMI { get; set; }
        public int? RespiratoryRate { get; set; }
        public int? NumberOfCigarettesPerDay { get; set; }
        public string GeneralFeeling { get; set; }
        public string Comments { get; set; }
        public DateTime DateAdded { get; set; }
        public int AppoinmentId { get; set; }
        public int PacientId { get; set; }
    }
}