using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.DTOs.Vaccines;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /* [Authorize] */
    public class VaccinesController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public VaccinesController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("required")]
        public async Task<ActionResult<IEnumerable<VaccineDto>>> GetAvailableVaccines()
        {
            var vaccines = await _context.Vaccines
            .ProjectTo<VaccineDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

            return Ok(vaccines);
        }

        [HttpGet("userVaccines/{pacientId}")]
        public async Task<ActionResult<IEnumerable<VaccineDto>>> GetUserVaccines(int pacientId)
        {
            var vaccinesXPacient = await _context.VaccineXPacients.Include(c => c.Vaccine)
            .Where(c => c.PacientId == pacientId)
            .OrderByDescending(c => c.DateAdded)
            .Select(c => new VaccineDto{
                Id = c.Vaccine.Id,
                RecommendedAge = c.Vaccine.RecommendedAge,
                Name = c.Vaccine.Name,
                For = c.Vaccine.For,
                Description = c.Vaccine.Description,
                IsRequired = c.Vaccine.IsRequired,
                PacientId = c.PacientId
            })
            .ToListAsync();

        
            return Ok(vaccinesXPacient);
        }

        [HttpPost("add")]
        public async Task<ActionResult> UpdatePacientVaccines(UpdatePacientVaccinesDto updatePacientVaccinesDto)
        {
            var pacient = await _context.Pacients
                .Include(c => c.ReceivedVaccines)
                .SingleOrDefaultAsync(c => c.Id == updatePacientVaccinesDto.PacientId);

            pacient.ReceivedVaccines.Clear();

            foreach (var vaccineDto in updatePacientVaccinesDto.Vaccines)
            {
                var vaccine = new VaccineXPacient();
                vaccine.PacientId = updatePacientVaccinesDto.PacientId;
                vaccine.VaccineId = vaccineDto.Id;
                _context.VaccineXPacients.Add(vaccine);
            }

            if(await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }

            return BadRequest("Ups..ceva nu a mers!");

        }
    }
}