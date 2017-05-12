using Aromato.Domain;
using Aromato.Domain.EmployeeAgg;
using Aromato.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;

namespace Aromato.Test.Infrastructure
{
    /// <summary>
    /// In-memory unit of work for the purpose of unit testing. This should not belong to the main assembly.
    /// </summary>
    public class InMemoryUnitOfWork : DbContext, IUnitOfWork
    {
        private const string DatabaseName = "aromata";

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(DatabaseName);
        }

        public void Commit()
        {
            SaveChanges();
        }

        public void Rollback()
        {
            Dispose();
        }
    }
}