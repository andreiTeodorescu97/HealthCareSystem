namespace API.Entities
{
    public class PacientGeneralMedicalData
    {
        public int Id { get; set; }
        public string BloodType { get; set; }
        public float WeightBirth { get; set; }
        public float HeightBirth { get; set; }
        public int NumberOfBirths { get; set; }
        public int NumberOfAvortions { get; set; }
        public bool IsSmoker { get; set; }
        public string GeneticDiseases { get; set; }
        public int PacientId { get; set; }
        public Pacient Pacient  { get; set; }
    }
}