using System;
using API.Entities;

namespace API.DTOs
{
    public class StudiesAndExperienceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}