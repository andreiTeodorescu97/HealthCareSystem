using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Consultations")]
    public class Consultation
    {
        public int Id { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public float? SystolicBp { get; set; }
        public float? DiastolicBp { get; set; }
        public float Temperature { get; set; }
        public int HeartRate { get; set; }
        public float? BloodSugar { get; set; }
        public float? BMI { get; set; }
        public int? RespiratoryRate { get; set; }
        public int? NumberOfCigarettesPerDay { get; set; }
        public string GeneralFeeling { get; set; }
        public string Comments { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public int AppoinmentId { get; set; }
        public Appoinment Appoinment { get; set; }
        public int PacientId { get; set; }
        public Pacient Pacient { get; set; }
        public bool? HasRecipe { get; set; }
        public Recipe Recipe { get; set; }
    }
}