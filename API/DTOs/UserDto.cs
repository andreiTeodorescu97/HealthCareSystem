namespace API.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Title { get; set; }
        public bool IsPacientAccount { get; set; }
        public string Token { get; set; }
    }
}