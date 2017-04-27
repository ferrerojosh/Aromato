using Aromato.Domain;
using Aromato.Domain.Aggregate;
using Microsoft.EntityFrameworkCore;

namespace Aromato.Infrastructure
{
    public class InMemoryUnitOfWork : DbContext, IUnitOfWork
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        public void Commit()
        {
            SaveChanges();
        }

        public void Rollback()
        {
            Dispose();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("aromato");
        }
    }
}