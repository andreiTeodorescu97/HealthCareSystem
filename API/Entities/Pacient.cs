using System;
using System.Collections.Generic;
using API.Extensions;

namespace API.Entities
{
    public class Pacient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string IdentityNumber { get; set; }
        public string Series { get; set; }
        public string CNP { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public PacientContact PacientContact { get; set; }
        public PacientGeneralMedicalData PacientGeneralMedicalData { get; set; }
        public ICollection<Appoinment> Appoinments { get; set; }
        public ICollection<Consultation> Consultations { get; set; }
        public ICollection<VaccineXPacient> ReceivedVaccines { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}