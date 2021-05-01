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
    public class PacientGeneralDataController : BaseApiController
    {
        private readonly IPacientRepository _pacientRepository;
        private readonly IMapper _mapper;
        public PacientGeneralDataController(IPacientRepository pacientRepository, IMapper mapper)
        {
            _mapper = mapper;
            _pacientRepository = pacientRepository;
        }

        [HttpPost]
        [Route("updateGeneralData")]
        public async Task<ActionResult> UpdatePacientGeneralData(PacientGeneralMedicalDataDto pacientGeneralMedicalDataDto)
        {
            if (await _pacientRepository.UpdatePacientGeneralMedicalData(pacientGeneralMedicalDataDto))
            {
                return NoContent();
            }

            return BadRequest("Upss...ceva nu a mers!");
        }


    }
}