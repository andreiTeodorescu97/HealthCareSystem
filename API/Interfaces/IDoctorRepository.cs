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
    }
}