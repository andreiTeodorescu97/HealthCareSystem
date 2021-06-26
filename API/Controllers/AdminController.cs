using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        public AdminController(UserManager<AppUser> userManager, DataContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users
                .Include(c => c.Doctor)
                .Include(p => p.Pacient)
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.Id,
                    FirstName = u.Doctor != null ? u.Doctor.FirstName : u.Pacient.FirstName,
                    SecondName = u.Doctor != null ? u.Doctor.SecondName : u.Pacient.SecondName,
                    Email = u.Doctor != null ? u.Doctor.Email : u.Pacient.Email,
                    Username = u.UserName,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList(),
                    IsAccountLocked = u.IsAccountLocked == null ? false : u.IsAccountLocked,
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("photos-to-moderate")]
        public ActionResult GetPhotosForModeration()
        {
            return Ok("Admins or moderators can see this");
        }


        [HttpPost("block")]
        public async Task<ActionResult> Block([FromQuery] string userName)
        {
            if (!User.IsInRole("Admin"))
            {
                return BadRequest("Nu aveti permisiunea!");
            }

            if (userName.Contains("admin"))
            {
                return BadRequest("Nu poti bloca acest cont!");
            }

            var user = await _context.Users
            .SingleOrDefaultAsync(x => x.UserName == userName);

            if (user == null)
            {
                return BadRequest("Upps...ceva nu a mers! Nu am gasit utilizatorul!");
            }

            user.IsAccountLocked = true;
            _context.Entry(user).State = EntityState.Modified;

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }

            return BadRequest("Upps...ceva nu a mers!");
        }

        [HttpPost("unblock")]
        public async Task<ActionResult> Unblock([FromQuery] string userName)
        {
            if (!User.IsInRole("Admin"))
            {
                return BadRequest("Nu aveti permisiunea!");
            }

            var user = await _context.Users
            .SingleOrDefaultAsync(x => x.UserName == userName);

            if (user == null)
            {
                return BadRequest("Upps...ceva nu a mers! Nu am gasit utilizatorul!");
            }

            user.IsAccountLocked = false;
            _context.Entry(user).State = EntityState.Modified;

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }

            return BadRequest("Upps...ceva nu a mers!");
        }
    }
}