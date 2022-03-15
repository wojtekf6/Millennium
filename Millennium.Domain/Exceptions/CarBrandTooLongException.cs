namespace Millennium.Domain.Exceptions
{
    public class CarBrandTooLongException : DomainException
    {
        public CarBrandTooLongException(string message) : base(message)
        {
        }
    }
}