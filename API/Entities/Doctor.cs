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
        public DateTime DateOfBirth { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<StudiesAndExperience> StudiesAndExperience { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}