using System;

namespace API.DTOs
{
    public class PrescriptionDto
    {
        public int Id { get; set; }
        public string DosageType { get; set; }
        public int DosageNumberPerDay { get; set; }
        public int Frequency { get; set; }
        public string FoodRelation { get; set; }
        public int NumberOfDays { get; set; }
        public string Route { get; set; }
        public string Instructions { get; set; }
        public DateTime DateAdded { get; set; }
        public int MedicineId { get; set; }
        public MedicineDto Medicine { get; set; }
    }
}