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
    public class PacientsRepository : IPacientRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PacientsRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<GetPacientDto>> GetPacientsUsingDtoAsync()
        {
            return await _context.Pacients
            .ProjectTo<GetPacientDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<GetPacientDto> GetPacientByCnpUsingDtoAsync(string cnp)
        { 
            return await _context.Pacients
            .Include(c => c.PacientContact.City.Region)
            .Select(c => new GetPacientDto()
            {
                FirstName = c.FirstName,
                SecondName = c.SecondName,
                Email = c.Email,
                IdentityNumber = c.IdentityNumber,
                Series = c.Series,
                CNP = c.CNP,
                DateOfBirth = c.DateOfBirth,
                Age = c.DateOfBirth.CalculateAge(),

                PacientContact = new PacientContactDto()
                {
                    FirstPhone = c.PacientContact.FirstPhone,
                    SecondPhone = c.PacientContact.SecondPhone,
                    Street = c.PacientContact.Street,
                    StreetNumber = c.PacientContact.StreetNumber,
                    CityId = c.PacientContact.City != null ? c.PacientContact.City.Id : null,
                    RegionId = c.PacientContact.City != null ? c.PacientContact.City.Region.Id : null
                }
            })
            .FirstOrDefaultAsync(c => c.CNP == cnp);
        }

        public async Task<GetPacientDto> GetPacientByFirstNameAsync(string firstName)
        {
            return await _context.Pacients
            .ProjectTo<GetPacientDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.FirstName.Contains(firstName));
        }

        public async Task<GetPacientDto> GetPacientByIdUsingDtoAsync(int id)
        {
            var pacient =   await _context.Pacients
            .Include(c => c.PacientContact.City.Region)
            .Include(c => c.PacientGeneralMedicalData)
            .FirstOrDefaultAsync(c => c.Id == id);

            var result = new GetPacientDto
            {
                FirstName = pacient.FirstName,
                SecondName = pacient.SecondName,
                Email = pacient.Email,
                IdentityNumber = pacient.IdentityNumber,
                Series = pacient.Series,
                CNP = pacient.CNP,
                DateOfBirth = pacient.DateOfBirth,
                Age = pacient.DateOfBirth.CalculateAge(),

                PacientContact = new PacientContactDto
                {
                    FirstPhone = pacient.PacientContact.FirstPhone,
                    SecondPhone = pacient.PacientContact.SecondPhone,
                    Street = pacient.PacientContact.Street,
                    StreetNumber = pacient.PacientContact.StreetNumber,
                    CityId = pacient.PacientContact.City != null ? pacient.PacientContact.City.Id : null,
                    RegionId = pacient.PacientContact.City != null ? pacient.PacientContact.City.Region.Id : null,
                    City = pacient.PacientContact.City != null ? pacient.PacientContact.City.Name : null,
                    Region = pacient.PacientContact.City != null ? pacient.PacientContact.City.Region.Name : null,
                },

                PacientGeneralMedicalData = new PacientGeneralMedicalDataDto
                {
                    BloodType = pacient.PacientGeneralMedicalData?.BloodType,
                    WeightBirth = pacient.PacientGeneralMedicalData?.WeightBirth,
                    HeightBirth = pacient.PacientGeneralMedicalData?.HeightBirth,
                    NumberOfBirths = pacient.PacientGeneralMedicalData?.NumberOfBirths,
                    NumberOfAvortions = pacient.PacientGeneralMedicalData?.NumberOfAvortions,
                    IsSmoker = pacient.PacientGeneralMedicalData?.IsSmoker.ToString()
                },
            };

            return result;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Pacient pacient)
        {
            _context.Entry(pacient).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Pacient>> GetPacientsAsync()
        {
            return await _context.Pacients.Include("PacientContact").ToListAsync();
        }

        public async Task<Pacient> GetPacientByCnpAsync(string cnp)
        {
            return await _context.Pacients.Include("PacientContact").SingleOrDefaultAsync(x => x.CNP == cnp);
        }

        public async Task<AppUser> GetPacientByUsername(string userName)
        {
            return await _context.Users
            .Include(c => c.Pacient.PacientContact.City.Region)
            .SingleOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<bool> UpdatePacientGeneralMedicalData(PacientGeneralMedicalDataDto pacientGeneralMedicalDataDto)
        {
            var pacient = await _context.Pacients.Include(c => c.PacientGeneralMedicalData)
            .SingleOrDefaultAsync(c => c.Id == pacientGeneralMedicalDataDto.PacientId);

            _mapper.Map(pacientGeneralMedicalDataDto, pacient.PacientGeneralMedicalData = new PacientGeneralMedicalData());

            _context.Entry(pacient).State = EntityState.Modified;

            return await SaveAllAsync();
        }
    }
}