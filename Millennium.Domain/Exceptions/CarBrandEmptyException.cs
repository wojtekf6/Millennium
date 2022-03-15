namespace Millennium.Domain.Exceptions
{
    public class CarBrandEmptyException : DomainException
    {
        public CarBrandEmptyException(string message) : base(message)
        {
        }
    }
}