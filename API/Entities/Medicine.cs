using System.Collections.Generic;

namespace API.Entities
{
    public class Medicine
    {
        public int Id { get; set; }
        public string CimCode { get; set; }
        public string CommercialName { get; set; }
        public string Name { get; set; }
        public string PharmaceuticalForm { get; set; }
        public string Concentration { get; set; }
        public string Producer { get; set; }
        public string TerapeuticalAction { get; set; }
        public string Valability { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}