using System;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface ILoggerService
    {
        Task<bool> LogError(Exception ex, string route = null);
    }
}