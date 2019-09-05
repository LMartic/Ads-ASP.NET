using Ads.Application.Dto;
using Ads.Application.Interfaces;
using Ads.Application.Searches;

namespace Ads.Application.Commands
{
    public interface IGetOfferByIdCommand : ICommand<OfferSearch, OfferDto>
    {

    }
}