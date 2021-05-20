using System;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public LoggerService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> LogError(Exception ex, string route = null)
        {
            var errorDto = new ErrorDto()
            {
                StatusCode = 200,
                Message = ex.Message,
                InnerMessage = ex.InnerException.Message,
                StackTrace = ex.StackTrace?.ToString(),
                Route = route,
                TimeStamp = DateTime.UtcNow,
            };

            var error = new Error();
            _mapper.Map(errorDto, error);
            _context.ChangeTracker.Clear();

            _context.Errors.Add(error);

            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }

            return false;
        }
    }
}