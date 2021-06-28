using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class PacientsHistoryController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PacientsHistoryController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IList<PacientHistoryDto>> Get()
        {
            var userId = User.GetUserId();

            var doctorId = _context.Doctors.FirstOrDefault(c => c.UserId == userId).Id;

            var result = await _context.PacientHistories
            .Include(c => c.Pacient)
            .Where(c => c.DoctorId == doctorId)
            .Select(c => new PacientHistoryDto {
                Id = c.Id,
                PacientId = c.PacientId,
                FirstName = c.Pacient.FirstName,
                SecondName = c.Pacient.SecondName,
                Email = c.Pacient.Email,
                Gender = c.Pacient.Gender,
                IdentityNumber = c.Pacient.IdentityNumber,
                Series= c.Pacient.Series,
                CNP = c.Pacient.CNP,
                DoctorId = c.DoctorId,
                DateOfBirth = c.Pacient.DateOfBirth,
                TotalNumberOfVisits = c.TotalNumberOfVisits,
                LastVisitDate = c.LastVisitDate
            })
            .ToListAsync();

            return result;
        }
    }
}