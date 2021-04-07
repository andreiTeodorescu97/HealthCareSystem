using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DoctorsController : BaseApiController
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        public DoctorsController(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _mapper = mapper;
            _doctorRepository = doctorRepository;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<UserDoctorDto>> GetDoctorByUsername(string userName)
        {
            var doctor = await _doctorRepository.GetDoctorByUsername(userName);
            doctor.Doctor.StudiesAndExperience.OrderBy(c => c.StartDate);
            return Ok(doctor);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDoctor(UserDoctorDto doctorDto)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var doctorUser = await _doctorRepository.GetDoctorByUsernameForUpdate(userName);

            doctorUser.Doctor.StudiesAndExperience.Clear();

            _mapper.Map(doctorDto.Doctor, doctorUser.Doctor);

            _doctorRepository.Update(doctorUser.Doctor);

            if (await _doctorRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Upss...ceva nu a mers!");
        }
    }
}