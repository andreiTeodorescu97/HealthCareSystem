namespace API.DTOs.Messages
{
    public class MessageParams
    {
        public string Username { get; set; }
        public string Container { get; set; } = "Unread";
    }
}