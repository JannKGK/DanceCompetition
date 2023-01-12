using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DanceCompetition.Models;
using System.Linq;

namespace DanceCompetition.Data
{
    public static class SeedData
    {

        public const string ROLE_ADMIN = "Admin";


        public static async Task SeedIdentity(UserManager<DanceCompetitionUser> userManager, RoleManager<DanceCompetitionRole> roleManager)
        {
            var user = await userManager.FindByNameAsync("kristjan.kessel@gmail.com");
            if (user == null)
            {
                user = new DanceCompetitionUser();
                user.Email = "kristjan.kessel@gmail.com";
                user.EmailConfirmed = true;
                user.UserName = "kristjan.kessel@gmail.com";
                var userResult = await userManager.CreateAsync(user);
                if (!userResult.Succeeded)
                {
                    throw new Exception($"User creation failed: {userResult.Errors.FirstOrDefault()}");
                }
                await userManager.AddPasswordAsync(user, "password");
            }
            var role = await roleManager.FindByNameAsync(ROLE_ADMIN);
            if (role == null)
            {
                role = new DanceCompetitionRole();
                role.Name = ROLE_ADMIN;
                role.NormalizedName = ROLE_ADMIN;
                var roleResult = roleManager.CreateAsync(role).Result;
                if (!roleResult.Succeeded)
                {
                    throw new Exception(roleResult.Errors.First().Description);
                }
            }
            await userManager.AddToRoleAsync(user, ROLE_ADMIN);
        }

    }
}
