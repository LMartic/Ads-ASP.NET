using Ads.Application.Commands;
using Ads.Application.Dto;
using Ads.Application.Searches;
using Ads.DataAccess.EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Ads.EfCommands.EntityFramework
{
    public class EfListAdCommand : EfCommand, IListAdCommand
    {
        public EfListAdCommand(AdsContext context) : base(context)
        {
        }

        public IEnumerable<ListAdDto> Execute(AdSearch request)
        {
            var ads = Context.Ads
                .Include(x => x.SubCategory)
                .ThenInclude(x => x.Category)
                .Include(x => x.User)
                .ToList().AsQueryable();

            if (!string.IsNullOrEmpty(request.Subject))
            {
                ads = ads.Where(w => w.Subject.ToLower().Contains(request.Subject.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.CategoryName))
                ads = ads.Where(s => s.SubCategory.Category.Name.ToLower() == request.CategoryName.ToLower());

            if (!string.IsNullOrEmpty(request.Description))
                ads = ads.Where(s => s.Description.ToLower().Contains(request.Description.ToLower()));

            if (!string.IsNullOrEmpty(request.SubCategoryName))
                ads = ads.Where(s => s.SubCategory.Name.ToLower() == request.SubCategoryName.ToLower());

            return ads.Select(s => new ListAdDto()
            {
                Id = s.Id,
                Subject = s.Subject,
                Description = s.Description,
                UserName = s.User.Email,
                CategoryName = s.SubCategory.Category.Name,
                SubCategoryName = s.SubCategory.Name
            }).ToList();
        }
    }
}