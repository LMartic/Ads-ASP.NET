using Ads.Application.Commands;
using Ads.Application.Dto;
using Ads.Application.Searches;
using Ads.DataAccess.EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Ads.EfCommands.EntityFramework
{
    public class EfListSubCategoryCommand : EfCommand, IListSubCategoryCommand
    {
        public EfListSubCategoryCommand(AdsContext context) : base(context)
        {
        }

        public IEnumerable<CreateSubCategoryDto> Execute(EmptySearch request)
        {
            var subCategories = Context.SubCategories.Include(x => x.Category).ToList();

            return subCategories.Select(s => new CreateSubCategoryDto()
            {
                Id = s.Id,
                Name = s.Name,
                CategoryId = s.CategoryId,
                CategoryName = s.Category.Name

            }).ToList();
        }
    }
}