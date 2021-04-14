using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class DoctorDto
    {
        public int Id {get; set;}
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public int Age {get;set;}
        public ICollection<StudiesAndExperienceDto> StudiesAndExperience { get; set; }
        public ICollection<WorkDayDto> WorkDays { get; set; }
    }
}