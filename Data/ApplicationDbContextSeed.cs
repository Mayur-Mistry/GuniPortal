using GuniPortal.Models;
using GuniPortal.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace GuniPortal.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedIdentityRolesAsync(RoleManager<MyIdentityRole> rolemanager)
        {
            foreach (MyIdentityRoleNames role in Enum.GetValues(typeof(MyIdentityRoleNames)))
            {
                string rolename = role.ToString();
                if (!await rolemanager.RoleExistsAsync(rolename))
                {
                    await rolemanager.CreateAsync(
                        new MyIdentityRole { Name = rolename });
                }
            }
        }


        public static async Task SeedIdentityUserAsync(UserManager<MyIdentityUser> usermanager)
        {
            MyIdentityUser user;

            user = await usermanager.FindByNameAsync("admin@gmail.com");
            if (user == null)
            {
                user = new MyIdentityUser()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    DisplayName = "The Admin User",
                    DateOfBirth = new DateTime(2000, 1, 1),
                    Gender = Genders.Female,
                    IsAdminUser = true,
                    Mobile_no = "8238298909"

                };
                await usermanager.CreateAsync(user, password: "Admin@123");
                await usermanager.AddToRolesAsync(user, new string[] {
                    MyIdentityRoleNames.Administrator.ToString(),
                    MyIdentityRoleNames.Faculty.ToString()
                });
            }

         
        }

    }
}
