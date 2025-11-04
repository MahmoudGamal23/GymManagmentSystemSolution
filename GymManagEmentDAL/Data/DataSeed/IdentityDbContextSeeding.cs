using GymManagementDAL.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.DataSeed
{
    public class IdentityDbContextSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            try
            {
                bool HasUsers = userManager.Users.Any();
                bool HasRoles = roleManager.Roles.Any();

                if (HasUsers && HasRoles) return false;

                if (!HasRoles)
                {
                    var Roles = new List<IdentityRole>()
                    {
                        new(){Name = "SuperAdmin"},
                        new(){Name = "Admin"}
                    };

                    foreach (var Role in Roles)
                    {
                        if (!roleManager.RoleExistsAsync(Role.Name!).Result)
                        {
                            roleManager.CreateAsync(Role).Wait();
                        }
                    }
                }
                if (!HasUsers)
                {
                    var MainAdmin = new ApplicationUser()
                    {
                        FirstName = "Mahmoud",
                        LastName = "Gamal",
                        UserName = "MahmoudGamal",
                        Email = "MahmoudGamal@gmail.com",
                        PhoneNumber = "01202884452"
                    };

                    userManager.CreateAsync(MainAdmin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(MainAdmin, "SuperAdmin").Wait();

                    var Admin01 = new ApplicationUser()
                    {
                        FirstName = "Gamal",
                        LastName = "SaadEldin",
                        UserName = "GamalSaadEldin",
                        Email = "GamalSaadeldin@gmail.com",
                        PhoneNumber = "01202884421"
                    };

                    userManager.CreateAsync(Admin01, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(Admin01, "Admin").Wait();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding Failed : {ex}");
                return false;
            }
        }
    }
}
