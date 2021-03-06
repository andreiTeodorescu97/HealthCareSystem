namespace API.DTOs
{
    public class DoctorGridDto
    {
        public int Id {get;set;}
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public int Age { get; set; }
        public string MainPhotoUrl { get; set; }
    }
}