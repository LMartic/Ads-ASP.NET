using Ads.Application.Commands;
using Ads.Application.Dto;
using Ads.DataAccess.Domain;
using Ads.DataAccess.EfDataAccess;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Ads.EfCommands.EntityFramework
{
    public class EfDeleteAdCommand : EfCommand, IDeleteAdCommand
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EfDeleteAdCommand(AdsContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public void Execute(DeleteAdDto request)
        {
            var user = _userManager.FindByIdAsync(request.UserId).Result;
            var role = _userManager.IsInRoleAsync(user, "Admin").Result;

            var adExist = Context.Ads.SingleOrDefault(x => x.Id == request.Id);

            if (!role && request.UserId != adExist.UserId)
                throw new UnauthorizedAccessException();

            Context.Remove(adExist);
            Context.SaveChanges();
        }
    }
}