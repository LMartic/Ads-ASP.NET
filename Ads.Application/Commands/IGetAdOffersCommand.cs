using Ads.Application.Dto;
using Ads.Application.Interfaces;
using Ads.Application.Searches;
using System.Collections.Generic;

namespace Ads.Application.Commands
{
    public interface IGetAdOffersCommand : ICommand<AdOfferSearch, IEnumerable<AdOffersListDto>>
    {

    }
}