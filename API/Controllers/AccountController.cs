using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.Include("Doctor").Include("Pacient").SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("Credentiale invalide!");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Parola incorecta!");
                }
            }

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user),
                FirstName = user.Doctor != null ? user.Doctor.FirstName : user.Pacient.FirstName,
                SecondName = user.Doctor != null ? user.Doctor.SecondName : user.Pacient.SecondName,
                Title = user.Doctor != null ? "Dr." : null,
                CNP = user.Doctor != null ? null : user.Pacient.CNP,
                IsPacientAccount = user.Doctor == null ? true : false
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (await UserExists(registerDto.Username))
            {
                return BadRequest("Acest username exista deja!");
            }

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
                DateCreated = DateTime.Now,
                Pacient = registerDto.IsPacientAccount ? new Pacient
                {
                    FirstName = registerDto.pacientDto.FirstName,
                    SecondName = registerDto.pacientDto.SecondName,
                    Email = registerDto.pacientDto.FirstName,
                    Gender = registerDto.pacientDto.Gender,
                    IdentityNumber = registerDto.pacientDto.IdentityNumber,
                    Series = registerDto.pacientDto.Series,
                    CNP = registerDto.pacientDto.CNP,
                    DateOfBirth = registerDto.pacientDto.DateOfBirth,
                } : null,
                Doctor = !registerDto.IsPacientAccount ? new Doctor
                {
                    FirstName = registerDto.doctorDto.FirstName,
                    SecondName = registerDto.doctorDto.SecondName,
                    Email = registerDto.doctorDto.Email,
                    DateOfBirth = registerDto.doctorDto.DateOfBirth,
                    HasWorkDays = false,
                } : null
            };

            try
            {
                _context.Users.Add(user);

                await _context.SaveChangesAsync();
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
                Token = _tokenService.CreateToken(user),
                CNP = registerDto.IsPacientAccount == true ? user.Pacient.CNP : null,
                IsPacientAccount = registerDto.IsPacientAccount
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(c => c.UserName == username.ToLower());
        }
    }
}