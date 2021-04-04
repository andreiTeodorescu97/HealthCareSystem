using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IPacientRepository
    {
        void Update(Pacient pacient);
        Task<bool> SaveAllAsync();
        Task<GetPacientDto> GetPacientByIdUsingDtoAsync(int id);
        Task<GetPacientDto> GetPacientByFirstNameAsync(string firstName);
        Task<IEnumerable<GetPacientDto>> GetPacientsUsingDtoAsync();
        Task<GetPacientDto> GetPacientByCnpUsingDtoAsync(string cnp);
        Task<IEnumerable<Pacient>> GetPacientsAsync();
        Task<Pacient> GetPacientByCnpAsync(string cnp);
        Task<AppUser> GetPacientByUsername(string userName);
    }
}