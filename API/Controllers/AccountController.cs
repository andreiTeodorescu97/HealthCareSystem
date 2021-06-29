using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using API.Data;
using API.DTOs;
using API.Email;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        public SignInManager<AppUser> _signInManager;
        private readonly IMailService _mailService;
        private readonly IConfiguration _config;


        public AccountController(DataContext context, ITokenService tokenService, UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager, IMailService mailService,
        IConfiguration config)
        {
            _signInManager = signInManager;
            _mailService = mailService;
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
            _config = config;
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
            if (user?.IsAccountLocked == true)
            {
                return Unauthorized("Contul este blocat!");
            }
            if (!user.EmailConfirmed)
            {
                return Unauthorized("Te rugam sa validezi email-ul!");
            }

            var result = await _signInManager
            .CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Credentiale invalide!");

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
                Email = registerDto.IsPacientAccount ? registerDto.pacientDto.Email : registerDto.doctorDto.Email,
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

                if (result.Succeeded && roleResult.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, email = user.Email }, Request.Scheme);

                    if (!await _mailService.SendConfirmationMail(user.Email, confirmationLink))
                    {
                        return BadRequest("Email-ul introdus nu este valid!");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Nu exista acest utilizator!");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return Ok("Email-ul a fost confirmat! Va multumim!");
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordDto forgotPassword)
        {
            if (forgotPassword.Email == null) return BadRequest("Te rog adauga email-ul!");

            var user = await _userManager.Users
                        .Where(e => e.Email.ToLower() == forgotPassword.Email.ToLower())
                        .FirstOrDefaultAsync();

            if (user == null) return Unauthorized("Username not Found");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string urlPath = "";
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower() == "development")
            {
                urlPath = _config["returnPaths:PasswordChange"];
            }
            else
            {
                urlPath = Environment.GetEnvironmentVariable("ReturnPaths:PasswordChange");
            }

            var changePasswordLink = BuildUrl(urlPath, token, user.Id.ToString());

            await _mailService.SendResetPasswordLink(user.Email, changePasswordLink);

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            var user = await _userManager.FindByIdAsync(resetPassword.UserId);
            if (user == null) return Unauthorized("Username-ul nu a fost gasit!");

            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (result.Succeeded) return Ok();

            return BadRequest("Upps...nu am putut reseta parola!");
        }

        private static string BuildUrl(string urlPath, string token, string userId)
        {
            var uriBuilder = new UriBuilder(urlPath);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = token;
            query["userid"] = userId;
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(c => c.UserName == username.ToLower());
        }

        private async Task<bool> EmailExists(string email, bool IsPacientAccount)
        {
            return await _context.Users.AnyAsync(c => c.Email == email);
        }

        private async Task<bool> CnpExists(string cnp)
        {
            return await _context.Pacients.AnyAsync(c => c.IdentityNumber == cnp);
        }
    }
}