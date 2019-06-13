using System;

namespace Ads.Application.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string entityType)
            : base(entityType + " already exists.")
        {

        }
    }
}