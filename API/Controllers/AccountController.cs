using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        public SignInManager<AppUser> _signInManager;
        public AccountController(DataContext context, ITokenService tokenService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.Include(c => c.Doctor.Photos)
            .Include(p => p.Pacient)
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null)
            {
                return Unauthorized("Credentiale invalide!");
            }

            var result = await _signInManager
            .CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            var userDto = new UserDto
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Id = user.Doctor != null ? user.Doctor.Id : user.Pacient.Id,
                FirstName = user.Doctor != null ? user.Doctor.FirstName : user.Pacient.FirstName,
                SecondName = user.Doctor != null ? user.Doctor.SecondName : user.Pacient.SecondName,
                Title = user.Doctor != null ? "Dr." : null,
                CNP = user.Doctor != null ? null : user.Pacient.CNP,
                IsPacientAccount = user.Doctor == null ? true : false,
            };

            if (user.Doctor != null)
            {
                var mainPhoto = user.Doctor.Photos.Where(c => c.IsMain == true).FirstOrDefault();
                userDto.MainPhotoUrl = mainPhoto?.Url;
            }

            return userDto;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (await UserExists(registerDto.Username))
            {
                return BadRequest("Acest username exista deja!");
            }

            if (registerDto.IsPacientAccount)
            {
                if (await EmailExists(registerDto.pacientDto.Email, true))
                {
                    return BadRequest("Acest email exista deja!");
                }
                else if (await CnpExists(registerDto.pacientDto.IdentityNumber))
                {
                    return BadRequest("Acest CNP exista deja!");
                }
            }
            else if (await EmailExists(registerDto.doctorDto.Email, false))
            {
                return BadRequest("Acest email exista deja!");
            }

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                DateCreated = DateTime.UtcNow,
                Pacient = registerDto.IsPacientAccount ? new Pacient
                {
                    FirstName = registerDto.pacientDto.FirstName,
                    SecondName = registerDto.pacientDto.SecondName,
                    Email = registerDto.pacientDto.Email,
                    Gender = registerDto.pacientDto.Gender,
                    IdentityNumber = registerDto.pacientDto.IdentityNumber,
                    Series = registerDto.pacientDto.Series,
                    CNP = registerDto.pacientDto.CNP,
                    DateOfBirth = DateTime.ParseExact(registerDto.pacientDto.DateOfBirth, "yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture),
                } : null,
                Doctor = !registerDto.IsPacientAccount ? new Doctor
                {
                    FirstName = registerDto.doctorDto.FirstName,
                    SecondName = registerDto.doctorDto.SecondName,
                    Email = registerDto.doctorDto.Email,
                    DateOfBirth = DateTime.ParseExact(registerDto.doctorDto.DateOfBirth, "yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture),
                    HasWorkDays = false,
                } : null
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (!result.Succeeded) return BadRequest(result.Errors);

                var roleResult = await _userManager.AddToRoleAsync(user, registerDto.IsPacientAccount ? "Pacient" : "Doctor");

                if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return new UserDto
            {

                UserName = user.UserName,
                Title = registerDto.IsPacientAccount == true ? null : "Dr.",
                FirstName = registerDto.IsPacientAccount ? user.Pacient.FirstName : user.Doctor.FirstName,
                SecondName = registerDto.IsPacientAccount ? user.Pacient.SecondName : user.Doctor.SecondName,
                Token = await _tokenService.CreateToken(user),
                CNP = registerDto.IsPacientAccount == true ? user.Pacient.CNP : null,
                IsPacientAccount = registerDto.IsPacientAccount
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(c => c.UserName == username.ToLower());
        }

        private async Task<bool> EmailExists(string email, bool IsPacientAccount)
        {
            if (IsPacientAccount)
            {
                return await _context.Pacients.AnyAsync(c => c.Email == email);
            }

            return await _context.Doctors.AnyAsync(c => c.Email == email);
        }

        private async Task<bool> CnpExists(string cnp)
        {
            return await _context.Pacients.AnyAsync(c => c.IdentityNumber == cnp);
        }
    }
}