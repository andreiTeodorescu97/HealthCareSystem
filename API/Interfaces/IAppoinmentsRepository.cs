using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface IAppoinmentsRepository
    {
         Task<IEnumerable<FreeHourDto>> GetAvailableHours(int doctorId, DateTime date);
         Task<IEnumerable<GetAppoimnetsDto>> GetPacientAppoinments(int pacientId);
         Task<IEnumerable<GetAppoimnetsDto>> GetDoctorAppoinments(int doctorId);
         Task<bool> AddAppoinmentAsync(MakeAnAppoinmentDto makeAnAppoinmentDto, int pacientId);
         Task<bool> UpdateAppoinmentStatus(UpdateAppoinmentStatusDto updateAppoinmentStatusDto);
    }
}