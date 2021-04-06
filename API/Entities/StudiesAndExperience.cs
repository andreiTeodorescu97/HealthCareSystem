using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("StudiesAndExperiences")]
    public class StudiesAndExperience
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

    }
}