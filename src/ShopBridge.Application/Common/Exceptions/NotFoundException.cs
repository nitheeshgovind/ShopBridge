using System;

namespace ShopBridge.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) 
            : base(message)
        {
        }

        public NotFoundException(string entity, object id)
            : base($"{entity} was not found for the key {id}")
        {            
        }
    }
}
