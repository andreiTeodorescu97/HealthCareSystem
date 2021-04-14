using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class DoctorsRepository : IDoctorRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public DoctorsRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserDoctorDto> GetDoctorByUsername(string userName)
        {
            return await _context.Users
                .ProjectTo<UserDoctorDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.UserName == userName);
        }

        public void Update(Doctor doctor)
        {
            _context.Entry(doctor).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<AppUser> GetDoctorByUsernameForUpdate(string userName)
        {
            //eager loading
            return await _context.Users.Include(p => p.Doctor.StudiesAndExperience)
            .Include(p => p.Doctor.WorkDays)
            .SingleOrDefaultAsync(c => c.UserName == userName);
        }

        public async Task<IEnumerable<WorkDayDto>> GetWorkDays(int doctorId)
        {
            return await _context.WorkDays
            .Where(c => c.DoctorId == doctorId)
            .ProjectTo<WorkDayDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<int> GetDoctorId(int userId)
        {
            var user = await _context.Users.Include(p => p.Doctor).SingleOrDefaultAsync(c => c.Id == userId);
            return user.Doctor.Id;
        }

        public async Task<IEnumerable<DoctorGridDto>> GetDoctors()
        {
            return await _context.Doctors
            /*             .Where(d => d.HasWorkDays == true) */
            .Select(c => new DoctorGridDto()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                SecondName = c.SecondName,
                Email = c.Email,
                DateOfBirth = c.DateOfBirth.ToString(),
                Age = c.DateOfBirth.CalculateAge(),
            })
            .ToListAsync();
        }

        public async Task<DoctorDto> GetDoctorByDoctorId(int doctorId)
        {
            return await _context.Doctors
            .ProjectTo<DoctorDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Id == doctorId);
        }
    }
}