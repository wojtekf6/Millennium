using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Millennium.Api.Controllers;
using Millennium.Data;
using Millennium.Data.UnitOfWork;
using Millennium.Domain.DTO;
using Millennium.Domain.Entities;
using Millennium.Domain.Exceptions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Millennium.Tests
{
    // Car controller test
    // Millennium.Domain.Test project should be added too and all domain functions should be tested e.g. MarkAsDeleted etc.
    public class Tests
    {
        private ILogger _logger;
        private Context _context;
        private UnitOfWork _unitOfWork;
        private CarController _carController;
        
        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            
            
            _logger = factory.CreateLogger<CarController>();
            _context = new InMemoryContext();
            _unitOfWork = new UnitOfWork(_context);
            _carController = new CarController(_unitOfWork, _logger);
        }
        
        [TearDown]
        public void Dispose()
        {
            _unitOfWork.Cars.RemoveRange(_unitOfWork.Cars);
            _unitOfWork.Commit();
        }

        [Test]
        public async Task ShouldAddCarCorrectly()
        {
            // adding car should be move to function
            await _carController.AddCar(new AddCarDto
            {
                Brand = "Audi",
                Model = "A4"
            });
            
            Assert.AreEqual("Audi", _unitOfWork.Cars.First().Brand);
            Assert.AreEqual("A4", _unitOfWork.Cars.First().Model);
        }
        
        [Test]
        public async Task ShouldGetAllCars()
        {
            await _carController.AddCar(new AddCarDto
            {
                Brand = "Audi",
                Model = "A4"
            });
            
            var cars = await _carController.GetCars();
            
            Assert.AreEqual("Audi", cars.First().Brand);
            Assert.AreEqual("A4", cars.First().Model);
        }
        
        [Test]
        public async Task ShouldDeleteCarCorrectly()
        {
            await _carController.AddCar(new AddCarDto
            {
                Brand = "Audi",
                Model = "A4"
            });
            
            var cars = await _carController.GetCars();
            
            Assert.AreEqual("Audi", cars.First().Brand);
            Assert.AreEqual("A4", cars.First().Model);

            var id = cars.First().Id;

            await _carController.DeleteCar(id);
            
            var cars2 = await _carController.GetCars();
            
            Assert.IsEmpty(cars2);
        }
    }
}