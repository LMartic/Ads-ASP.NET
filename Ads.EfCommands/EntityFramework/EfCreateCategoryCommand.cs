using Ads.Application.Commands;
using Ads.Application.Dto;
using Ads.Application.Exceptions;
using Ads.DataAccess.Domain;
using Ads.DataAccess.EfDataAccess;
using System.Linq;

namespace Ads.EfCommands.EntityFramework
{
    public class EfCreateCategoryCommand : EfCommand, ICreateCategoryCommand
    {
        public EfCreateCategoryCommand(AdsContext context) : base(context)
        {
        }

        public void Execute(CreateCategoryDto request)
        {
            if (Context.Categories.Any(x => x.Name == request.Name))
                throw new EntityAlreadyExistsException("Category");

            Context.Categories.Add(new Category()
            {
                Name = request.Name
            });
            Context.SaveChanges();
        }
    }
}