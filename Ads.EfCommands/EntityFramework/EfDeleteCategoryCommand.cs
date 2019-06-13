using Ads.Application.Commands;
using Ads.Application.Dto;
using Ads.Application.Exceptions;
using Ads.DataAccess.EfDataAccess;
using System.Linq;

namespace Ads.EfCommands.EntityFramework
{
    public class EfDeleteCategoryCommand : EfCommand, IDeleteCategoryCommand
    {
        public EfDeleteCategoryCommand(AdsContext context) : base(context)
        {
        }

        public void Execute(CreateCategoryDto request)
        {
            if (!Context.Categories.Any(s => s.Id == request.Id))
                throw new EntityNotFoundException("Category");

            var category = Context.Categories.SingleOrDefault(w => w.Id == request.Id);

            Context.Categories.Remove(category);
            Context.SaveChanges();
        }
    }
}