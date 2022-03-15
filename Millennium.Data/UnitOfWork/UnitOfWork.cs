using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Millennium.Domain.Entities;

namespace Millennium.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        // repositories
        public DbSet<Car> Cars => _context.Cars;

        public UnitOfWork(Context context)
        {
            _context = context;
        }
        
        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}