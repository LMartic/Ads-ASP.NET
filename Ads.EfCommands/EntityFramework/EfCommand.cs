using Ads.DataAccess.EfDataAccess;

namespace Ads.EfCommands.EntityFramework
{
    public class EfCommand
    {
        protected readonly AdsContext Context;

        protected EfCommand(AdsContext context)
        {
            Context = context;
        }
    }
}