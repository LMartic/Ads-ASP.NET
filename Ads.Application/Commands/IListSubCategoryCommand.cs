using Ads.Application.Dto;
using Ads.Application.Interfaces;
using Ads.Application.Searches;
using System.Collections.Generic;

namespace Ads.Application.Commands
{
    public interface IListSubCategoryCommand : ICommand<EmptySearch, IEnumerable<CreateSubCategoryDto>>
    {

    }


    public interface IListSubCategoryPerCategoryCommand : ICommand<EmptySearch, IEnumerable<CreateSubCategoryDto>>
    {

    }

}