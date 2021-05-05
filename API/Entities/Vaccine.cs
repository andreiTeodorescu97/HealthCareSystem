using System.Collections.Generic;

namespace API.Entities
{
    public class Vaccine
    {
        public int Id { get; set; }
        public string RecommendedAge { get; set; }
        public string Name { get; set; }
        public string For { get; set; }
        public string Description { get; set; }
        public bool IsCustomForUser { get; set; } = false;
        public bool IsRequired { get; set; }
        public ICollection<VaccineXPacient> Pacients { get; set; }
    }
}