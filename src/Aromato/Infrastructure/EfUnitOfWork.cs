using System;
using Aromato.Domain;
using Aromato.Domain.Aggregate;
using Microsoft.EntityFrameworkCore;

namespace Aromato.Infrastructure
{
    public class EfUnitOfWork : DbContext, IUnitOfWork
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        public void Commit()
        {
            this.SaveChanges();
        }

        public void Rollback()
        {
            this.Dispose();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("aromato");
        }
    }
}