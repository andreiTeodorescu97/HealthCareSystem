using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class DoctorsController : BaseApiController
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly DataContext _context;
        public DoctorsController(IDoctorRepository doctorRepository, IMapper mapper, IPhotoService photoService, DataContext context)
        {
            _context = context;
            _photoService = photoService;
            _mapper = mapper;
            _doctorRepository = doctorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorGridDto>>> GetDoctors()
        {
            var doctors = await _doctorRepository.GetDoctors();
            return Ok(doctors);
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<UserDoctorDto>> GetDoctorByUsername(string userName)
        {
            var doctor = await _doctorRepository.GetDoctorByUsername(userName);
            doctor.Doctor.StudiesAndExperience = doctor.Doctor.StudiesAndExperience.OrderByDescending(c => c.StartDate).ToList();
            return Ok(doctor);
        }

        [HttpGet("getdoctor",  Name = "GetDoctor")]
        public async Task<ActionResult<DoctorDto>> GetDoctorByDoctorId(int doctorId)
        {
            var doctor = await _doctorRepository.GetDoctorByDoctorId(doctorId);
            doctor.StudiesAndExperience = doctor.StudiesAndExperience.OrderByDescending(c => c.StartDate).ToList();
            return Ok(doctor);
        }

        [HttpGet("work-days")]
        public async Task<ActionResult<IEnumerable<WorkDayDto>>> GetWorkDays()
        {

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
                item.StartHour = item.StartHour;
                item.EndHour = item.EndHour;
                var workDay = new WorkDay();
                _mapper.Map(item, workDay);
                doctorUser.Doctor.WorkDays.Add(workDay);
            }

            if (doctorUser.Doctor.WorkDays.Count() > 0)
            {
                doctorUser.Doctor.HasWorkDays = true;
            }
            else
            {
                doctorUser.Doctor.HasWorkDays = false;
            }

            _doctorRepository.Update(doctorUser.Doctor);

            if (await _doctorRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Upss...ceva nu a mers!");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var username = User.GetUserName();

            var user = await _context.Users
            .Include(c => c.Doctor.Photos)
            .FirstOrDefaultAsync(c => c.UserName == username);

            if (user.Doctor == null || file == null)
            {
                return BadRequest("Invalid data!");
            }

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (user.Doctor.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            user.Doctor.Photos.Add(photo);

            if (await _context.SaveChangesAsync() > 0)
            {
                return CreatedAtRoute("GetDoctor", new { username = user.UserName },
                 _mapper.Map<PhotoDto>(photo));
            }

            return BadRequest("Problem adding photo!");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var username = User.GetUserName();

            var user = await _context.Users
            .Include(c => c.Doctor.Photos)
            .FirstOrDefaultAsync(c => c.UserName == username);

            if (user.Doctor == null)
            {
                return BadRequest("Invalid data!");
            }

            var photo = user.Doctor.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo.IsMain)
            {
                return BadRequest("This is already your main photo!");
            }

            var currentMain = user.Doctor.Photos.FirstOrDefault(c => c.IsMain);

            if (currentMain != null)
            {
                currentMain.IsMain = false;
                photo.IsMain = true;
            }

            if (await _context.SaveChangesAsync() > 0) return NoContent();

            return BadRequest("Failed to set main photo!");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var username = User.GetUserName();

            var user = await _context.Users
            .Include(c => c.Doctor.Photos)
            .FirstOrDefaultAsync(c => c.UserName == username);

            if (user.Doctor == null)
            {
                return BadRequest("Invalid data!");
            }

            var photo = user.Doctor.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("You cannot delete your main photo!");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            user.Doctor.Photos.Remove(photo);

            if (await _context.SaveChangesAsync() > 0) return Ok();

            return BadRequest("Failed to delete the photo");
        }
    }
}