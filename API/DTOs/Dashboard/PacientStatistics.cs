using System;

namespace API.DTOs.Dashboard
{
    public class PacientStatisticsDto
    {
        public string LastAppoinmentDate { get; set; }
        public int NumberOfDaysFromLastAppoinment { get; set; }
        public int NumberOfRecipes { get; set; }
        public int NumberOfAppoinments { get; set; }
        public int LastHeight { get; set; }
        public int LastWeight { get; set; }
        public float LastTemperature { get; set; }
        public int LastHeartRate { get; set; }
    }
}