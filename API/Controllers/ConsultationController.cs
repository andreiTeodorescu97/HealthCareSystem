using System.Threading.Tasks;
using API.DTOs;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /* [Authorize] */
    public class ConsultationController : BaseApiController
    {
        private readonly IConsultationRepository _consultationRepository;
        public ConsultationController(IConsultationRepository consultationRepository)
        {
            _consultationRepository = consultationRepository;
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddConsultation(ConsultationDto consultationDto)
        {
            if(await _consultationRepository.AddConsultationAsync(consultationDto))
            {
                return Ok();
            }

            return BadRequest("Upps..ceva nu a mers!");
        }


    }
}