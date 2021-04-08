using System;

namespace API.DTOs
{
    public class WorkDayDto
    {
        public string Day { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime EndHour { get; set; }
    }
}