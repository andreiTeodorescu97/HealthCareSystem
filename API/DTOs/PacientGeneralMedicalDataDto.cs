namespace API.DTOs
{
    public class PacientGeneralMedicalDataDto
    {
        public string BloodType { get; set; }
        public float WeightBirth { get; set; }
        public float HeightBirth { get; set; }
        public int NumberOfBirths { get; set; }
        public int NumberOfAvortions { get; set; }
        public bool IsSmoker { get; set; }
        public string GeneticDiseases { get; set; }
    }
}