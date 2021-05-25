using System.Collections.Generic;

namespace API.DTOs.Vaccines
{
    public class UpdatePacientVaccinesDto
    {
        public ICollection<VaccineDto> Vaccines { get; set; }
        public int PacientId { get; set; }
    }
}