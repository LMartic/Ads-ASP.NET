using Ads.Application.Interfaces;
using Ads.DataAccess.Domain;

namespace Ads.Application.Commands
{
    public interface IGetAddByIdCommand : ICommand<int, Ad>
    {

    }
}