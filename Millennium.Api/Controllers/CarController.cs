using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Millennium.Data.UnitOfWork;
using Millennium.Domain.DTO;
using Millennium.Domain.Entities;

namespace Millennium.Api.Controllers
{
    [ApiController]
    [Route( "api/cars" )]
    public class CarController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        
        public CarController(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        
        // GET: api/car
        [HttpGet]
        public async Task<List<GetCarDto>> GetCars()
        {
            _logger.LogInformation("Get all cars");
            var cars = await _unitOfWork.Cars.AsNoTracking().NotDeleted().PresentedOnListAsync();
            return cars;
        }
        
        // GET: api/car/id
        [HttpGet("{id}", Name = nameof(GetCar))]
        public async Task<ActionResult<GetCarDto>> GetCar(Guid id)
        {
            _logger.LogInformation("Get car with id", id);
            var car = await _unitOfWork.Cars.AsNoTracking().NotDeleted().WithIdAsync(id);

            if (car == null)
            {
                _logger.LogError("Car not found");
                return NotFound();
            }

            return car.Present();
        }
        
        // POST: api/car
        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] AddCarDto dto)
        {
            _logger.LogInformation("Add new car");
            
            var car = new Car(dto);
            
            await _unitOfWork.Cars.AddAsync(car);
            await _unitOfWork.SaveChangesAsync();
            
            return CreatedAtRoute(nameof(GetCar), new { id = car.Id }, car.Present());
        }
        
        // DELETE: api/car/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            _logger.LogInformation("Delete car with id", id);
            var car = await _unitOfWork.Cars.NotDeleted().WithIdAsync(id);

            if (car == null)
            {
                _logger.LogError("Car not found");
                return NotFound();
            }
            
            car.MarkAsDeleted();
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
        
    }
}