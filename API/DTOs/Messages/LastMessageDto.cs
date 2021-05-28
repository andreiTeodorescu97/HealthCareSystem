using System.Collections.Generic;

namespace API.DTOs.Messages
{
    public class LastMessageDto
    {
        public string Username { get; set; }
        public MessageDto Message { get; set; }
    }
}