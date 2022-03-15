namespace Millennium.Domain.Exceptions
{
    public class CarModelEmptyException : DomainException
    {
        public CarModelEmptyException(string message) : base(message)
        {
        }
    }
}