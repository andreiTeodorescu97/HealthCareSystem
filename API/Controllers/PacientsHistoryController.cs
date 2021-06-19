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
            .Where(c => c.DoctorId == doctorId)
            .ProjectTo<PacientHistoryDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

            return result;
        }
    }
}