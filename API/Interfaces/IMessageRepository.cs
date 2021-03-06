using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.Messages;
using API.Entities;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
        void AddGroup(Group group);
        void RemoveConnection(Connection connection);
        Task<Connection> GetConnection(string connectionId);
        Task<Group> GetMessageGroup(string groupName);
        Task<Group> GetGroupForConnection(string connectionId);
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessage(int id);
        Task<IList<LastMessageDto>> GetAllLastMessages(string loggedInUsername);
        Task<IList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
        Task<IList<SimpleUserDto>> GetUsers();
        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername);
        Task<bool> SaveAllAsync();
    }
}