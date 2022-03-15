using Microsoft.EntityFrameworkCore;
using Millennium.Domain.Entities;

namespace Millennium.Data
{
    public class Context : DbContext
    {
        public DbSet<Car> Cars { get; init; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //TODO: move connection string to appsettings.json
            options.UseSqlite("Data Source = database.db");
        }
    }
}