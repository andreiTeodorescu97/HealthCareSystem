using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.DTOs.Filters;
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

        public async Task<IEnumerable<DoctorGridDto>> GetDoctors(DoctorFilterDto doctorFilterDto)
        {
            var query = _context.Doctors.Include(c => c.Photos).AsQueryable();

            query = query.Where(c => c.User.IsAccountLocked != true);

            if (doctorFilterDto.Id != 0)
            {
                query = query.Where(c => c.Id == doctorFilterDto.Id);
            }
            else
            {
                if (!string.IsNullOrEmpty(doctorFilterDto.FirstName))
                {
                    query = query.Where(c => c.FirstName.Contains(doctorFilterDto.FirstName));
                }
                if (!string.IsNullOrEmpty(doctorFilterDto.SecondName))
                {
                    query = query.Where(c => c.SecondName.Contains(doctorFilterDto.SecondName));
                }
                if (!string.IsNullOrEmpty(doctorFilterDto.Email))
                {
                    query = query.Where(c => c.Email == doctorFilterDto.Email);
                }
            }

            var result = await query.OrderBy(c => c.Id)
            .Select(c => new DoctorGridDto()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                SecondName = c.SecondName,
                UserName = c.User.UserName,
                Email = c.Email,
                DateOfBirth = c.DateOfBirth.ToString(),
                Age = c.DateOfBirth.CalculateAge(),
                MainPhotoUrl = c.Photos.FirstOrDefault(c => c.IsMain == true).Url
            })
            .ToListAsync();

            if (doctorFilterDto.Age != 0)
            {
                result = result.Where(c => c.Age == doctorFilterDto.Age).ToList();;
            }

            return result;
        }

        public async Task<DoctorDto> GetDoctorByDoctorId(int doctorId)
        {
            var doctor = await _context.Doctors.Where(x => x.Id == doctorId)
            .ProjectTo<DoctorDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

            return doctor;
        }
    }
}