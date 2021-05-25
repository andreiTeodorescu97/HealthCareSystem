using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.Messages;
using API.Entities;
using API.Interfaces;

namespace API.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        public MessageRepository(DataContext context)
        {
            _context = context;

        }

        public void AddMessage(Message message)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteMessage(Message message)
        {
            throw new System.NotImplementedException();
        }

        public Task<Message> GetMessage(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}