namespace API.DTOs.Admin
{
    public class AdminUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public bool IsAccountLocked { get; set; }
    }
}