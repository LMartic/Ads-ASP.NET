using Ads.Application.Dto;
using Ads.Application.Interfaces;
using Ads.Application.Searches;
using System.Collections.Generic;

namespace Ads.Application.Commands
{
    public interface IGetCategoriesCommand : ICommand<CategorySearch, IEnumerable<CategoryListDto>>
    {

    }
}