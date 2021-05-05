using System;

namespace API.Entities
{
    public class VaccineXPacient
    {
        public int Id { get; set; }
        public int PacientId { get; set; }
        public Pacient Pacient { get; set; }
        public int VaccineId { get; set; }
        public Vaccine Vaccine { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}