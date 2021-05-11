using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public Guid? UniqueId { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public int ConsultationId { get; set; }
        public Consultation Consultation { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
        public int PacientId { get; set; }
    }
}