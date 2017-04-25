using Aromato.Domain.Aggregate;
using Microsoft.EntityFrameworkCore;

namespace Aromato.Infrastructure
{
    public class AromatoContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=aromato.db");
        }
    }
}