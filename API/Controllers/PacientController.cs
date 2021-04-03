using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /*  [Authorize] */
    public class PacientsController : BaseApiController
    {
        private readonly IPacientRepository _pacientRepository;
        public PacientsController(IPacientRepository pacientRepository)
        {
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


    }
}