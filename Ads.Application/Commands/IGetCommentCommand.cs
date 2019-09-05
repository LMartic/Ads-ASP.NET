using Ads.Application.Interfaces;
using Ads.DataAccess.Domain;

namespace Ads.Application.Commands
{
    public interface IGetCommentCommand : ICommand<int, Comment>
    {

    }
}