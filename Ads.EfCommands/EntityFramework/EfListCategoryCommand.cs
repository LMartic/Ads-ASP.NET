using Ads.Application.Commands;
using Ads.Application.Dto;
using Ads.Application.Searches;
using Ads.DataAccess.EfDataAccess;
using System.Collections.Generic;
using System.Linq;

namespace Ads.EfCommands.EntityFramework
{
    public class EfListCategoryCommand : EfCommand, IListCategoryCommand
    {
        public EfListCategoryCommand(AdsContext context) : base(context)
        {
        }

        public IEnumerable<CreateCategoryDto> Execute(EmptySearch request)
        {
            var categories = Context.Categories.ToList();

            return categories.Select(s => new CreateCategoryDto()
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }
    }
}