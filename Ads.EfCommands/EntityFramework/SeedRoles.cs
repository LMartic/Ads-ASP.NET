using Ads.DataAccess.EfDataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ads.EfCommands.EntityFramework
{
    public class SeedRoles
    {
        public static async Task SeedAsync(AdsContext context)
        {
            if (!context.Roles.Any())
            {
                foreach (var role in GetPreconfiguredRoles())
                {
                    await context.Roles.AddRangeAsync(new IdentityRole() { Name = role, NormalizedName = role.ToUpper() });
                }

                await context.SaveChangesAsync();
            }
        }

        static IEnumerable<string> GetPreconfiguredRoles()
        {
            return new List<string>()
            {
                "Admin",
                "Member",
            };
        }
    }
}