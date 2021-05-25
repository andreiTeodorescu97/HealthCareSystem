namespace API.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string CNP { get; set; }
        public string Title { get; set; }
        public bool IsPacientAccount { get; set; }
        public string Token { get; set; }
        public string MainPhotoUrl { get; set; }
    }
}