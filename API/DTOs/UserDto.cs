namespace API.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public bool IsDoctor { get; set; }
        public string Token { get; set; }
    }
}