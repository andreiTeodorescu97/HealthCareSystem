using System;

namespace API.Entities
{
    public class Prescription
    {
        public int Id { get; set; }
        public string DosageType { get; set; }
        public int DosageNumberPerDay { get; set; }
        public int Frequency { get; set; }
        public string FoodRelation { get; set; }
        public int NumberOfDays { get; set; }
        public string Route { get; set; }
        public string Instructions { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}