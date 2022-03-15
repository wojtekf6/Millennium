using Microsoft.EntityFrameworkCore;
using Millennium.Data;

namespace Millennium.Tests
{
    public class InMemoryContext : Context
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase(databaseName: "Test");
        }
    }
}