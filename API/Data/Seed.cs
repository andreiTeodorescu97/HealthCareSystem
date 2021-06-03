

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var doctorData = await System.IO.File.ReadAllTextAsync("Data/DoctorSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            var doctors = JsonSerializer.Deserialize<List<AppUser>>(doctorData);

            if (users == null || doctors == null) return;

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Pacient"},
                new AppRole{Name = "Moderator"},
                new AppRole{Name = "Doctor"},
                new AppRole{Name = "Admin"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                user.DateCreated = DateTime.Now;
                user.EmailConfirmed = true;
                await userManager.CreateAsync(user, "1234!Asd");
                await userManager.AddToRoleAsync(user, "Pacient");
            }

            foreach (var doctor in doctors)
            {
                doctor.UserName = doctor.UserName.ToLower();
                doctor.DateCreated = DateTime.Now;
                doctor.EmailConfirmed = true;
                await userManager.CreateAsync(doctor, "1234!Asd");
                await userManager.AddToRoleAsync(doctor, "Doctor");
            }

            var admin = new AppUser
            {
                UserName = "admin",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(admin, "1234!Asd");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });

        }
    }
}