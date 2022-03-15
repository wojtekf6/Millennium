using System;

namespace Millennium.Domain.DTO
{
    public record GetCarDto
    {
        public Guid Id { get; set; }
        
        public string Brand { get; set; } 
        
        public string Model { get; set; } 
    }
}