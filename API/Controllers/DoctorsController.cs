using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DoctorsController : BaseApiController
    {
        private readonly IDoctorRepository _doctorRepository;
        public DoctorsController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<UserDoctorDto>> GetDoctorByUsername(string userName)
        {
            var doctor = await _doctorRepository.GetDoctorByUsername(userName);
            return Ok(doctor);
        }
    }
}