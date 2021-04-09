

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var doctorData = await System.IO.File.ReadAllTextAsync("Data/DoctorSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            var doctors = JsonSerializer.Deserialize<List<AppUser>>(doctorData);

            if (users == null || doctors == null) return;

            /*             var roles = new List<AppRole> 
                        {
                            new AppRole{Name = "Member"},
                            new AppRole{Name = "Admin"},
                            new AppRole{Name = "Moderator"},
                        };

                        foreach(var role in roles)
                        {
                            await roleManager.CreateAsync(role);
                        }

                        foreach (var user in users)
                        {
                            user.UserName = user.UserName.ToLower();
                            await userManager.CreateAsync(user,"1234!Asd");
                            await userManager.AddToRoleAsync(user, "Member");
                        } */

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("1234!Asd"));
                user.PasswordSalt = hmac.Key;
                user.DateCreated = DateTime.Now;
                context.Users.Add(user);
            }
                await context.SaveChangesAsync();
                
            foreach (var doctor in doctors)
            {
                using var hmac = new HMACSHA512();
                doctor.UserName = doctor.UserName.ToLower();
                doctor.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("1234!Asd"));
                doctor.PasswordSalt = hmac.Key;
                doctor.DateCreated = DateTime.Now;
                context.Users.Add(doctor);
            }

            await context.SaveChangesAsync();


            /*             var admin = new AppUser
                        {
                            UserName = "admin"
                        };

                        await userManager.CreateAsync(admin, "1234!Asd");
                        await userManager.AddToRolesAsync(admin, new[] {"Admin", "Moderator"}); */

        }
    }
}