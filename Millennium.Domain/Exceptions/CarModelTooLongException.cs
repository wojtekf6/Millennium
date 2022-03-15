namespace Millennium.Domain.Exceptions
{
    public class CarModelTooLongException : DomainException
    {
        public CarModelTooLongException(string message) : base(message)
        {
        }
    }
}