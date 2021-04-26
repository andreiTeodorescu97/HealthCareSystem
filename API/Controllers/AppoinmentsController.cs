using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /* [Authorize] */
    public class AppoinmentsController : BaseApiController
    {
        private readonly IAppoinmentsRepository _appoinmentsRepository;
        private readonly IPacientRepository _pacientRepository;
        private readonly IDoctorRepository _doctorRepository;
        public AppoinmentsController(IAppoinmentsRepository appoinmentsRepository, IPacientRepository pacientRepository,
        IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
            _pacientRepository = pacientRepository;
            _appoinmentsRepository = appoinmentsRepository;
        }

        [HttpGet("getHours")]
        public async Task<ActionResult<IEnumerable<FreeHourDto>>> GetFreeHours(int doctorId, long unixTime)
        {
            if (unixTime == 0 || doctorId == 0)
            {
                return BadRequest("Parametrii invalizi!");
            }
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTime);
            var availableSlots = await _appoinmentsRepository.GetAvailableHours(doctorId, dtDateTime);
            return Ok(availableSlots);
        }

        [HttpPost("add")]
        public async Task<ActionResult> MakeAnAppoinment(MakeAnAppoinmentDto makeAnAppoinmentDto)
        {
            if (makeAnAppoinmentDto == null
            || makeAnAppoinmentDto.DoctorId == 0
            || makeAnAppoinmentDto.DayUnixTime == 0
            || makeAnAppoinmentDto.FromTimeSpan == 0 || makeAnAppoinmentDto.ToTimeSpan == 0)
            {
                return BadRequest("Parametrii invalizi!");
            }

            var userPacient = await _pacientRepository.GetPacientByUsername(User.GetUserName());

            if (await _appoinmentsRepository.AddAppoinmentAsync(makeAnAppoinmentDto, userPacient.Pacient.Id))
            {
                return Ok();
            }

            return BadRequest("Upps..ceva nu a mers!");
        }

        [HttpGet("pacientAppoinmets")]
        public async Task<ActionResult<IEnumerable<GetAppoimnetsDto>>> GetPacientAppoinments()
        {
            var userPacient = await _pacientRepository.GetPacientByUsername(User.GetUserName());

            return Ok(await _appoinmentsRepository.GetPacientAppoinments(userPacient.Id));
        }

        [HttpGet("doctorAppoinmets")]
        public async Task<ActionResult<IEnumerable<GetAppoimnetsDto>>> GetDoctorAppoinments()
        {
            var doctorId = await _doctorRepository.GetDoctorId(User.GetUserId());

            return Ok(await _appoinmentsRepository.GetDoctorAppoinments(doctorId));
        }
    }
}