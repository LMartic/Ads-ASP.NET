using System;

namespace Ads.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entity) : base($"{entity} doesn't exist.")
        {

        }

        public EntityNotFoundException()
        {

        }
    }
}