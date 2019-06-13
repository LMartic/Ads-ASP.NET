using Ads.Application.Commands;
using Ads.Application.Dto;
using Ads.Application.Exceptions;
using Ads.DataAccess.Domain;
using Ads.DataAccess.EfDataAccess;
using System.Linq;

namespace Ads.EfCommands.EntityFramework
{
    public class EfCreateSubCategoryCommand : EfCommand, ICreateSubCategoryCommand
    {
        public EfCreateSubCategoryCommand(AdsContext context) : base(context)
        {
        }

        public void Execute(CreateSubCategoryDto request)
        {
            if (Context.SubCategories.Any(x => x.Name == request.Name))
                throw new EntityAlreadyExistsException("SubCategory");

            var categoryExist = Context.Categories.Any(x => x.Id == request.CategoryId);

            if (!categoryExist)
                throw new EntityNotFoundException("Category");

            Context.SubCategories.Add(new SubCategory()
            {
                Name = request.Name,
                CategoryId = request.CategoryId
            });

            Context.SaveChanges();
        }
    }
}