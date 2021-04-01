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
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

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

                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
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
                Pacient = registerDto.IsPacientAccount ? new Pacient
                {
                    FirstName = registerDto.pacientDTO.FirstName,
                    SecondName = registerDto.pacientDTO.SecondName,
                    Email = registerDto.pacientDTO.FirstName,
                    Gender = registerDto.pacientDTO.Gender,
                    IdentityNumber = registerDto.pacientDTO.IdentityNumber,
                    Series = registerDto.pacientDTO.Series,
                    CNP = registerDto.pacientDTO.CNP,
                    DateOfBirth = registerDto.pacientDTO.DateOfBirth,
                } : null,
                Doctor = !registerDto.IsPacientAccount ? new Doctor
                {
                    FirstName = registerDto.doctorDTO.FirstName,
                    SecondName = registerDto.doctorDTO.SecondName,
                    Email = registerDto.doctorDTO.Email,
                    DateOfBirth = registerDto.doctorDTO.DateOfBirth,
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

                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            }; ;
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(c => c.UserName == username.ToLower());
        }
    }
}