using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.Messages;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class MessageController : BaseApiController
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public MessageController(IMessageRepository messageRepository, IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
            _messageRepository = messageRepository;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetUserName();

            if (username == createMessageDto.RecipientUsername.ToLower())
                return BadRequest("You cannot send messages to yourself!");

            var sender = await _context.Users.FirstOrDefaultAsync(c => c.UserName == username);
            var recipient = await _context.Users.FirstOrDefaultAsync(c => c.UserName == createMessageDto.RecipientUsername);

            if (recipient == null) return NotFound();

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content,
                MessageSent = DateTime.UtcNow
            };

            _messageRepository.AddMessage(message);

            if (await _messageRepository.SaveAllAsync())
                return Ok(_mapper.Map<MessageDto>(message));

            return BadRequest("Failed to send message!");
        }

        [HttpGet]
        public async Task<ActionResult<IList<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            messageParams.Username = User.GetUserName();

            var messages = await _messageRepository.GetMessagesForUser(messageParams);

            return Ok(messages);
        }

        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        {
            var currentUsername = User.GetUserName();

            return Ok(await _messageRepository.GetMessageThread(currentUsername, username));
        }

        [HttpGet("last-messages")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetAllLastMessages()
        {
            var currentUsername = User.GetUserName();

            return Ok(await _messageRepository.GetAllLastMessages(currentUsername));
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<SimpleUserDto>>> GetAllUsers()
        {
            return Ok(await _messageRepository.GetUsers());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var username = User.GetUserName();

            var message = await _messageRepository.GetMessage(id);

            if (message.Sender.UserName != username && message.Recipient.UserName != username)
                return Unauthorized();

            if (message.Sender.UserName == username) message.SenderDeleted = true;

            if (message.Recipient.UserName == username) message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted)
                _messageRepository.DeleteMessage(message);

            if (await _messageRepository.SaveAllAsync()) return Ok();

            return BadRequest("Problem deleting the message");
        }

    }
}