using Aromato.Domain;
using Aromato.Domain.Employee;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(DatabaseName);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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