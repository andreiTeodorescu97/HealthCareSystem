using System;
using API.Entities;

namespace API.DTOs
{
    public class StudiesAndExperienceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}