using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class DoctorDto
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<StudiesAndExperienceDto> StudiesAndExperience { get; set; }
        public ICollection<WorkDayDto> WorkDays { get; set; }
    }
}