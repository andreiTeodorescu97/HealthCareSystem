using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IDoctorRepository
    {
        void Update(Doctor doctor);
        Task<bool> SaveAllAsync();
        Task<UserDoctorDto> GetDoctorByUsername (string userName);
        Task<AppUser> GetDoctorByUsernameForUpdate (string userName);
        Task<int> GetDoctorId (int userId);
        Task<IEnumerable<WorkDayDto>> GetWorkDays (int userId);
        Task<IEnumerable<DoctorGridDto>> GetDoctors();
        Task<DoctorDto> GetDoctorByDoctorId (int doctorId);
    }
}