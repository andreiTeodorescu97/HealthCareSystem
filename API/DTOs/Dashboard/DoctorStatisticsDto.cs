namespace API.DTOs.Dashboard
{
    public class DoctorStatisticsDto
    {
        public int NumberOfAppoinmentsToday { get; set; }
        public int NumberOfPacients { get; set; }
        public int NumberOfReviews { get; set; }
         public int NumberOfFinalizedAppoinments { get; set; }
    }
}