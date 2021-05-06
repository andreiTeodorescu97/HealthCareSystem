using System.Collections.Generic;

namespace API.DTOs
{
    public class UpdatePacientVaccinesDto
    {
        public ICollection<VaccineDto> Vaccines { get; set; }
        public int PacientId { get; set; }
    }
}