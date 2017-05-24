using System;
using Aromato.Domain;
using Aromato.Domain.CredentialAgg;
using Aromato.Domain.EmployeeAgg;
using Aromato.Domain.InventoryAgg;
using Aromato.Domain.RoleAgg;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(employee =>
            {
                employee.HasOne(e => e.Credential).WithOne(c => c.Employee)
                    .HasForeignKey<Employee>("credential_id");
            });

            modelBuilder.Entity<Credential>(credential =>
            {
                credential.HasOne(c => c.Employee).WithOne(e => e.Credential)
                    .HasForeignKey<Employee>("employee_id");
            });

            modelBuilder.Entity<EmployeeRole>(empRole =>
            {
                empRole.Property<long>("employee_id").UseNpgsqlSerialColumn();
                empRole.Property<long>("role_id").UseNpgsqlSerialColumn();
                empRole.HasKey("employee_id", "role_id");
            });

            modelBuilder.Entity<RolePermission>(rolePerm =>
            {
                rolePerm.Property<long>("permission_id").UseNpgsqlSerialColumn();
                rolePerm.Property<long>("role_id").UseNpgsqlSerialColumn();
                rolePerm.HasKey("role_id", "permission_id");
            });
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