using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /* [Authorize] */
    public class PacientsController : BaseApiController
    {
        private readonly IPacientRepository _pacientRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public PacientsController(IPacientRepository pacientRepository, IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
            _pacientRepository = pacientRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPacientDto>>> GetPacients()
        {
            var pacients = await _pacientRepository.GetPacientsUsingDtoAsync();

            return Ok(pacients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPacientDto>> GetPacientAsync(int id)
        {

            if (User.IsInRole("Doctor"))
            {
                var userId = User.GetUserId();
                var doctorId = _context.Doctors.FirstOrDefault(c => c.User.Id == userId).Id;
                var pacientHasAppoinmentToDoctor = _context.Appoinments.Any(c => c.DoctorId == doctorId && c.PacientId == id);

                if (!pacientHasAppoinmentToDoctor)
                {
                    return Unauthorized("Accesul este interzis pentru acest pacient!");
                }
            }
            else if (User.IsInRole("Pacient"))
            {
                var userId = User.GetUserId();
                var pacientIdFromDb = _context.Pacients.FirstOrDefault(c => c.User.Id == userId).Id;

                if (id != pacientIdFromDb)
                {
                    return Unauthorized("Accesul este interzis pentru acest pacient!");
                }
            }

            var pacient = await _pacientRepository.GetPacientByIdUsingDtoAsync(id);

            return Ok(pacient);
        }

        [HttpGet("find/{cnp}")]
        public async Task<ActionResult<GetPacientDto>> GetPacient(string cnp)
        {
            var pacient = await _pacientRepository.GetPacientByCnpUsingDtoAsync(cnp);

            return Ok(pacient);
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePacient(GetPacientDto pacientDto)
        {
            var userName = User.GetUserName();

            var pacient = await _pacientRepository.GetPacientByUsername(userName);

            pacient.Pacient.FirstName = pacientDto.FirstName;
            pacient.Pacient.SecondName = pacientDto.SecondName;
            pacient.Pacient.User.Email = pacientDto.Email;
            pacient.Pacient.Email = pacientDto.Email;

            if(pacient.Pacient.PacientContact == null)
            {
                pacient.Pacient.PacientContact = new Entities.PacientContact();
            }
            
            pacient.Pacient.PacientContact.Street = pacientDto.PacientContact.Street;
            pacient.Pacient.PacientContact.StreetNumber = pacientDto.PacientContact.StreetNumber;
            pacient.Pacient.PacientContact.FirstPhone = pacientDto.PacientContact.FirstPhone;
            pacient.Pacient.PacientContact.SecondPhone = pacientDto.PacientContact.SecondPhone;
            pacient.Pacient.PacientContact.CityId = pacientDto.PacientContact.CityId;

            _pacientRepository.Update(pacient.Pacient);

            if (await _pacientRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Upss...ceva nu a mers!");
        }
    }
}