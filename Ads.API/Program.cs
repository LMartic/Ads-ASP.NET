using Ads.DataAccess.Domain;
using Ads.DataAccess.EfDataAccess;
using Ads.EfCommands.EntityFramework;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Ads.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AdsContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                SeedRoles.SeedAsync(context).Wait();
                SeedAdminUser.SeedAsync(context, userManager).Wait();
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
