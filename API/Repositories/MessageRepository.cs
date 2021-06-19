using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.Messages;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MessageRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages
                .Include(u => u.Sender)
                .Include(u => u.Recipient)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _context.Messages
                .OrderByDescending(m => m.MessageSent)
                .AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.Recipient.UserName == messageParams.Username &&
                u.RecipientDeleted == false),
                "Outbox" => query.Where(u => u.Sender.UserName == messageParams.Username &&
                u.SenderDeleted == false),
                _ => query.Where(u => u.Recipient.UserName == messageParams.Username &&
                    u.RecipientDeleted == false && u.DateRead == null)
            };

            var messages = await query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider).ToListAsync();

            return messages;
        }

        public async Task<IList<LastMessageDto>> GetAllLastMessages(string loggedInUsername)
        {
            var loggedInUser = await _context.Users
                .Where(c => c.UserName == loggedInUsername)
                .SingleOrDefaultAsync();

            var receivers = await _context.Messages
                 .Where(c => c.SenderUsername == loggedInUsername)
                 .Select(c => c.RecipientUsername)
                 .ToListAsync();

            var senders = await _context.Messages
                .Where(c => c.RecipientUsername == loggedInUsername)
                .Select(c => c.SenderUsername)
                .ToListAsync();


            var allUsers = senders.Concat(receivers).Distinct();

            var result = new List<LastMessageDto>();

            foreach (var user in allUsers)
            {
                var messageForUser = await _context.Messages
                    .Include(u => u.Sender).ThenInclude(p => p.Doctor.Photos)
                    .Include(u => u.Recipient).ThenInclude(p => p.Doctor.Photos)
                .Where(m => m.RecipientUsername == user
                && m.SenderUsername == loggedInUsername
                && m.RecipientDeleted == false
                || m.RecipientUsername == loggedInUsername
                && m.SenderUsername == user
                && m.SenderDeleted == false
                ).OrderByDescending(m => m.MessageSent)
                 .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync();

                result.Add(new LastMessageDto
                {
                    Username = user,
                    Message = messageForUser
                });
            }

            return result;
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
        {
            var messages = await _context.Messages
            .Include(u => u.Sender).ThenInclude(p => p.Doctor.Photos)
            .Include(u => u.Recipient).ThenInclude(p => p.Doctor.Photos)
                .Where(m => m.RecipientUsername == currentUsername
                && m.SenderUsername == recipientUsername
                && m.RecipientDeleted == false
                || m.RecipientUsername == recipientUsername
                && m.SenderUsername == currentUsername
                && m.SenderDeleted == false
                ).OrderBy(m => m.MessageSent)
                .ToListAsync();

            var unreadMessages = messages.Where(m => m.DateRead == null
                && m.Recipient.UserName == currentUsername).ToList();

            if (unreadMessages.Any())
            {
                foreach (var unreadMessage in unreadMessages)
                {
                    unreadMessage.DateRead = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }



        public async Task<bool> SaveAllAsync()

        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<SimpleUserDto>> GetUsers()
        {
            var pacients = await _context.Users.Include(c => c.Pacient)
            .Where(c => c.Pacient != null)
            .Select(c => new SimpleUserDto
            {
                Id = c.Id,
                UserName = c.UserName,
                FirstName = c.Pacient.FirstName,
                SecondName = c.Pacient.SecondName
            }).ToListAsync();

            var doctors = await _context.Users.Include(c => c.Doctor)
            .Where(c => c.Doctor != null)
            .Select(c => new SimpleUserDto
            {
                Id = c.Id,
                UserName = c.UserName,
                FirstName = c.Doctor.FirstName,
                SecondName = c.Doctor.SecondName
            }).ToListAsync();

            return pacients
            .Concat(doctors)
            .OrderBy(c => c.Id)
            .ToList();
        }

        public void AddGroup(Group group)
        {
            _context.Groups.Add(group);
        }

        public void RemoveConnection(Connection connection)
        {
            _context.Connections.Remove(connection);
        }

        public async Task<Connection> GetConnection(string connectionId)
        {
            return await _context.Connections.FindAsync(connectionId);
        }

        public async Task<Group> GetMessageGroup(string groupName)
        {
            return await _context.Groups
            .Include(c => c.Connections)
            .FirstOrDefaultAsync(c => c.Name == groupName);
        }

        public async Task<Group> GetGroupForConnection(string connectionId)
        {
            return await _context.Groups
            .Include(c => c.Connections)
            .Where(c => c.Connections.Any(x => x.ConnectionId == connectionId))
            .FirstOrDefaultAsync();
        }
    }
}