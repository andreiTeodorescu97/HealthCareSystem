using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Motto { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool HasWorkDays { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<StudiesAndExperience> StudiesAndExperience { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<WorkDay> WorkDays { get; set; }
        public ICollection<Appoinment> Appoinments { get; set; }
        public ICollection<PacientHistory> PacientHistories { get; set; }
    }
}