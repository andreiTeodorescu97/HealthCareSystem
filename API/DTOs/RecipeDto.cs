using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class RecipeDto
    {
        public Guid? UniqueId { get; set; }
        public DateTime DateAdded { get; set; }
        public int ConsultationId { get; set; }
        public ICollection<PrescriptionDto> Prescriptions { get; set; }
        public int PacientId { get; set; }
    }
}