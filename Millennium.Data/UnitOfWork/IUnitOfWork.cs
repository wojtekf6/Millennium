using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Millennium.Domain.Entities;

namespace Millennium.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        DbSet<Car> Cars { get; }
        
        int Commit();
        
        Task<int> SaveChangesAsync();
    }
}