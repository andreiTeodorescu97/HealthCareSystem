using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface IAppoinmentsRepository
    {
         Task<IEnumerable<FreeHourDto>> GetAvailableHours(int doctorId, DateTime date);
         Task<bool> AddAppoinmentAsync(MakeAnAppoinmentDto makeAnAppoinmentDto, int pacientId);
    }
}