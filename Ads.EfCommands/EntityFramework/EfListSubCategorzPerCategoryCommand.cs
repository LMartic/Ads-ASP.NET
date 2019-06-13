using Ads.Application.Commands;
using Ads.Application.Dto;
using Ads.Application.Searches;
using Ads.DataAccess.EfDataAccess;
using System.Collections.Generic;
using System.Linq;

namespace Ads.EfCommands.EntityFramework
{
    public class EfListSubCategorzPerCategoryCommand : EfCommand, IListSubCategoryPerCategoryCommand
    {

        public IEnumerable<CreateSubCategoryDto> Execute(EmptySearch request)
        {
            if (request.CategoryId > 0)
            {
                return Context.SubCategories
                    .Where(w => w.CategoryId == request.CategoryId)
                    .Select(s => new CreateSubCategoryDto()
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToList();
            }
            return new List<CreateSubCategoryDto>();
        }

        public EfListSubCategorzPerCategoryCommand(AdsContext context) : base(context)
        {
        }
    }
}