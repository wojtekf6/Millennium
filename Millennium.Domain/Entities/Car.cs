using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Millennium.Domain.DTO;
using Millennium.Domain.Exceptions;

namespace Millennium.Domain.Entities
{
    public class Car : AuditableEntity
    {
        public Guid Id { get; private set; }
        
        public string Brand { get; private set; } 
        
        public string Model { get; private set; }

        public Car()
        {
            
        }
        
        public Car(AddCarDto carDto) : base()
        {
            Id = Guid.NewGuid();
            Brand = !string.IsNullOrEmpty(carDto.Brand) ? carDto.Brand.Trim() : carDto.Brand;
            Model = !string.IsNullOrEmpty(carDto.Model) ? carDto.Model.Trim() : carDto.Model;
            
            Validate();
        }

        public GetCarDto Present()
        {
            return new GetCarDto
            {
                Id = Id,
                Brand = Brand,
                Model = Model
            };
        }

        public void MarkAsDeleted()
        {
            DeletedAt = DateTime.UtcNow;
        }

        private void Validate()
        {
            // TODO create const for max values etc.
            if (string.IsNullOrEmpty(Brand)) throw new CarBrandEmptyException("Car brand is empty");
            if (string.IsNullOrEmpty(Model)) throw new CarModelEmptyException("Car model is empty");
            if (Brand.Length > 64) throw new CarBrandTooLongException("Car brand is too long");
            if (Model.Length > 64) throw new CarBrandTooLongException("Car model is too long");
        }
    }

    public static class CarsSpecification
    {
        public static async Task<List<GetCarDto>> PresentedOnListAsync(this IQueryable<Car> items)
        {
            return await items.Select(i => i.Present()).ToListAsync();
        }
        
        public static async Task<Car> WithIdAsync(this IQueryable<Car> items, Guid id)
        {
            return await items.FirstOrDefaultAsync( i => i.Id == id );
        }
        
        public static IQueryable<Car> NotDeleted(this IQueryable<Car> items)
        {
            return items.Where(i => !i.DeletedAt.HasValue);
        }
    }
}