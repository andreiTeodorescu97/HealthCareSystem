using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class DoctorsController : BaseApiController
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        public DoctorsController(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _mapper = mapper;
            _doctorRepository = doctorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorGridDto>>> GetDoctors()
        {
           var doctors =  await _doctorRepository.GetDoctors();
           return Ok(doctors);
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<UserDoctorDto>> GetDoctorByUsername(string userName)
        {
            var doctor = await _doctorRepository.GetDoctorByUsername(userName);
            doctor.Doctor.StudiesAndExperience = doctor.Doctor.StudiesAndExperience.OrderByDescending(c => c.StartDate).ToList();
            return Ok(doctor);
        }

        [HttpGet("getdoctor")]
        public async Task<ActionResult<DoctorDto>> GetDoctorByDoctorId(int doctorId)
        {
            var doctor = await _doctorRepository.GetDoctorByDoctorId(doctorId);
            doctor.StudiesAndExperience = doctor.StudiesAndExperience.OrderByDescending(c => c.StartDate).ToList();
            return Ok(doctor);
        }

        [HttpGet("work-days")]
        public async Task<ActionResult<IEnumerable<WorkDayDto>>> GetWorkDays(){
            
            var userId = User.GetUserId();
            var workDays = await _doctorRepository.GetWorkDays(await _doctorRepository.GetDoctorId(userId));
            return Ok(workDays); 
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDoctor(UserDoctorDto doctorDto)
        {
            var userName = User.GetUserName();

            var doctorUser = await _doctorRepository.GetDoctorByUsernameForUpdate(userName);

            doctorUser.Doctor.StudiesAndExperience.Clear();

            _mapper.Map(doctorDto.Doctor, doctorUser.Doctor);

            _doctorRepository.Update(doctorUser.Doctor);

            if (await _doctorRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Upss...ceva nu a mers!");
        }

        [HttpPost("add-work-days")]
        public async Task<ActionResult> AddWorkDays(ICollection<WorkDayDto> workDays)
        {
            var userName = User.GetUserName();

            var doctorUser = await _doctorRepository.GetDoctorByUsernameForUpdate(userName);

            doctorUser.Doctor.WorkDays.Clear();

            foreach (var item in workDays)
            {
                item.StartHour = item.StartHour.AddHours(3);
                item.EndHour = item.EndHour.AddHours(3);
                var workDay = new WorkDay();
                _mapper.Map(item, workDay);
                doctorUser.Doctor.WorkDays.Add(workDay);
            }

            if(doctorUser.Doctor.WorkDays.Count() > 0){
                doctorUser.Doctor.HasWorkDays = true;
            }else{
                doctorUser.Doctor.HasWorkDays = false;
            }

            _doctorRepository.Update(doctorUser.Doctor);

            if (await _doctorRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Upss...ceva nu a mers!");
        }
    }
}