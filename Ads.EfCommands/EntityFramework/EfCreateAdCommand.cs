using Ads.Application.Commands;
using Ads.Application.Dto;
using Ads.Application.Exceptions;
using Ads.Application.Interfaces;
using Ads.DataAccess.Domain;
using Ads.DataAccess.EfDataAccess;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Ads.EfCommands.EntityFramework
{
    public class EfCreateAdCommand : EfCommand, ICreateAdCommand
    {
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;

        public EfCreateAdCommand(AdsContext context, IEmailSender emailSender, UserManager<ApplicationUser> userManager) : base(context)
        {
            _emailSender = emailSender;
            _userManager = userManager;
        }

        public void Execute(CreateAdDto request)
        {
            if (!Context.SubCategories.Any(x => x.Id == request.SubCategoryId))
                throw new EntityNotFoundException("SubCategory");

            if (string.IsNullOrEmpty(request.Subject))
                throw new ApplicationException("Subject mora imate vrednost");

            Context.Ads.Add(new Ad()
            {
                AddedDateTime = DateTime.Now,
                UserId = request.UserId,
                Subject = request.Subject,
                Description = request.Description,
                SubCategoryId = request.SubCategoryId
            });

            Context.SaveChanges();
            var user = _userManager.FindByIdAsync(request.UserId);
            _emailSender.Subject = "Uspesno kreiran oglas";
            _emailSender.Body = "Uspesno ste kreirali oglas";
            _emailSender.ToEmail = "danijelboksan@gmail.com";
            _emailSender.Send();
        }
    }
}