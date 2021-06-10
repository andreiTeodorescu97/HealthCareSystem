using System;

namespace API.Entities
{
    public class PacientHistory
    {
        public int Id { get; set; }
        public int PacientId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string IdentityNumber { get; set; }
        public string Series { get; set; }
        public string CNP { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int TotalNumberOfVisits { get; set; }
        public DateTime LastVisitDate { get; set; }
    }
}