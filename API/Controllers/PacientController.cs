using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
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
        public PacientsController(IPacientRepository pacientRepository, IMapper mapper)
        {
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
           pacient.Pacient.Email = pacientDto.Email;
           pacient.Pacient.PacientContact.Street = pacientDto.PacientContact.Street;
           pacient.Pacient.PacientContact.StreetNumber = pacientDto.PacientContact.StreetNumber;
           pacient.Pacient.PacientContact.FirstPhone = pacientDto.PacientContact.FirstPhone;
           pacient.Pacient.PacientContact.SecondPhone = pacientDto.PacientContact.SecondPhone;
           pacient.Pacient.PacientContact.CityId = pacientDto.PacientContact.CityId;

            _pacientRepository.Update(pacient.Pacient);

            if(await _pacientRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Upss...ceva nu a mers!");
        }
    }
}