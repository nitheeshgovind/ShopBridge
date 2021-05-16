using System;

namespace ShopBridge.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message) 
            : base(message)
        {
        }

        public NotFoundException(string entity, string key, object id)
            : base($"{entity} was not found for the {key} {id}")
        {            
        }
    }
}
