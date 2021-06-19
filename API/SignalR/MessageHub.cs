using System;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.Messages;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace API.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHubContext<PresenceHub> _presenceHub;
        private readonly PresenceTracker _presenceTracker;
        public MessageHub(IMessageRepository messageRepository, IMapper mapper, DataContext context, IHubContext<PresenceHub> presenceHub, PresenceTracker presenceTracker)
        {
            _presenceTracker = presenceTracker;
            _presenceHub = presenceHub;
            _context = context;
            _mapper = mapper;
            _messageRepository = messageRepository;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"].ToString();
            var groupName = GetGroupName(Context.User.GetUserName(), otherUser);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var group = await AddToGroup(groupName);
            await Clients.Group(groupName).SendAsync("UpdatedGroup", group);

            var messsages = await _messageRepository
            .GetMessageThread(Context.User.GetUserName(), otherUser);

            await Clients.Caller.SendAsync("ReceiveMessageThread", messsages);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var group = await RemoveFromGroup();
            await Clients.Group(group.Name).SendAsync("UpdatedGroup", group);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(CreateMessageDto createMessageDto)
        {
            var username = Context.User.GetUserName();

            if (username == createMessageDto.RecipientUsername.ToLower())
                throw new HubException("You cannot send messages to yourself!");

            var sender = await _context.Users
                .Include(c => c.Doctor)
                .Include(c => c.Pacient)
                .FirstOrDefaultAsync(c => c.UserName == username);

            var recipient = await _context.Users
                .Include(c => c.Doctor)
                .Include(c => c.Pacient)
                .FirstOrDefaultAsync(c => c.UserName == createMessageDto.RecipientUsername);

            if (recipient == null) throw new HubException("User Not Found!");

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content,
                MessageSent = DateTime.UtcNow
            };

            var groupName = GetGroupName(sender.UserName, recipient.UserName);

            var group = await _messageRepository.GetMessageGroup(groupName);

            if (group.Connections.Any(c => c.Username == recipient.UserName))
            {
                message.DateRead = DateTime.UtcNow;
            }
            else
            {
                var connections = await _presenceTracker.GetConnectionsForUser(recipient.UserName);
                if (connections != null)
                {
                    await _presenceHub.Clients.Clients(connections).SendAsync("NewMessageNotification",
                        new
                        {
                            username = sender.UserName,
                            name = sender.Doctor != null ? "Dr. " +
                        sender.Doctor.FirstName + " " + sender.Doctor.SecondName : sender.Pacient.FirstName + " " + sender.Pacient.SecondName
                        });
                }
            }

            _messageRepository.AddMessage(message);

            if (await _messageRepository.SaveAllAsync())
            {
                await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));

            }
            else
            {
                throw new HubException("Failed to send message!");
            }
        }

        private async Task<Group> AddToGroup(string groupName)
        {
            var group = await _messageRepository.GetMessageGroup(groupName);
            var connection = new Connection(Context.ConnectionId, Context.User.GetUserName());

            if (group == null)
            {
                group = new Group(groupName);
                _messageRepository.AddGroup(group);
            }
            group.Connections.Add(connection);

            if (await _messageRepository.SaveAllAsync())
            {
                return group;
            }

            throw new HubException("Failed to join group!");
        }

        private async Task<Group> RemoveFromGroup()
        {
            var group = await _messageRepository.GetGroupForConnection(Context.ConnectionId);
            var connection = group.Connections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

            _messageRepository.RemoveConnection(connection);

            if (await _messageRepository.SaveAllAsync())
            {
                return group;
            }

            throw new HubException("Failed to remove from group!");
        }

        private string GetGroupName(string caller, string other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }
    }
}