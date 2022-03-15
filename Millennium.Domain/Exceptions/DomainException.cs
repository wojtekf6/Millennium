using System;

namespace Millennium.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public virtual string ErrorCode { get; }
        
        public DomainException(string message) : base (message)
        {
            
        }
    }
}