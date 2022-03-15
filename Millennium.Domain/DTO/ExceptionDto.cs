namespace Millennium.Domain.DTO
{
    public record ExceptionDto
    {
        public ExceptionDto(int code, string name, string message)
        {
            Code = code;
            Name = name;
            Message = message;
        }
        public int Code { get; init; }
        public string Name { get; init; }
        public string Message { get; init; }
    }
}