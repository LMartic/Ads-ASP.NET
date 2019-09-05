using Ads.Application.Dto;
using Ads.Application.Interfaces;
using System.Collections.Generic;

namespace Ads.Application.Commands
{
    public interface IGetAdCommentsCommand : ICommand<int, IEnumerable<CommentListDto>>
    {

    }
}