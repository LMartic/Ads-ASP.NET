using Ads.Application.Commands;
using Ads.Application.Dto;
using Ads.Application.Exceptions;
using Ads.DataAccess.Domain;
using Ads.DataAccess.EfDataAccess;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Ads.EfCommands.EntityFramework
{
    public class EfEditAdCommand : EfCommand, IEditAdCommand
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EfEditAdCommand(AdsContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public void Execute(EditAdDto request)
        {
            var user = _userManager.FindByIdAsync(request.UserId).Result;
            var role = _userManager.IsInRoleAsync(user, "Admin").Result;

            var adExist = Context.Ads.SingleOrDefault(x => x.Id == request.Id);

            if (!role && request.UserId != adExist.UserId)
                throw new UnauthorizedAccessException();


            if (adExist.Id <= 0)
                throw new EntityNotFoundException("Ad");

            if (!string.IsNullOrEmpty(request.Subject))
                adExist.Subject = request.Subject;

            if (!string.IsNullOrEmpty(request.Description))
                adExist.Description = request.Description;




            adExist.AddedDateTime = DateTime.Now;

            Context.SaveChanges();
        }
    }
}