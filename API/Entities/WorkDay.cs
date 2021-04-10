using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{

    [Table("WorkDays")]
    public class WorkDay
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime EndHour { get; set; }
        public int? StartTimeSpan{get;set;}
        public int? EndTimeSpan{get;set;}
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
