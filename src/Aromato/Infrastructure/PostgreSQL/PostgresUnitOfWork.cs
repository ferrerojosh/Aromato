﻿using Aromato.Domain;
using Aromato.Domain.Employee;
using Aromato.Domain.Inventory;
using Microsoft.EntityFrameworkCore;

namespace Aromato.Infrastructure.PostgreSQL
{
    public class PostgresUnitOfWork : DbContext, IUnitOfWork
    {
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = AromatoConfig.Instance;
            optionsBuilder.UseNpgsql(
                $"Host={config["database:postgres:host"]};" +
                $"Database={config["database:postgres:name"]};" +
                $"Username={config["database:postgres:user"]};" +
                $"Password={config["database:postgres:pass"]}"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(employee =>
            {
                employee.Property(e => e.Id).HasColumnName("id");
                employee.HasKey(e => e.Id).HasName("id");
                employee.HasAlternateKey(e => e.UniqueId);
                employee.Property(e => e.FirstName).HasColumnName("first_name");
                employee.Property(e => e.MiddleName).HasColumnName("middle_name");
                employee.Property(e => e.LastName).HasColumnName("last_name");
                employee.Property(e => e.Gender).HasColumnName("gender");
                employee.Property(e => e.ContactNo).HasColumnName("contact_number");
                employee.Property(e => e.Email).HasColumnName("email");
                employee.Property(e => e.UniqueId).HasColumnName("unique_id");
                employee.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");

                employee.ToTable("employee");
            });

            modelBuilder.Entity<Inventory>(inventory =>
            {
                inventory.Property(i => i.Id).HasColumnName("id");
                inventory.HasKey(i => i.Id);
                inventory.Property(i => i.Name).HasColumnName("name");
                inventory.Property(i => i.Description).HasColumnName("description");
                inventory.HasMany(i => i.Items).WithOne().HasForeignKey("inventory_id");

                inventory.ToTable("inventory");
            });

            modelBuilder.Entity<Item>(item =>
            {
                item.Property(i => i.Id).HasColumnName("id");
                item.HasKey(i => i.Id);
                item.HasAlternateKey(i => i.UniqueId);
                item.Property(i => i.UniqueId).HasColumnName("unique_id");
                item.Property(i => i.DateAdded).HasColumnName("date_added");
                item.Property(i => i.LastUpdated).HasColumnName("last_updated");
                item.Property(i => i.Status).HasColumnName("status");

                item.ToTable("item");
            });

            modelBuilder.Entity<Punch>(punch =>
            {
                punch.Property(p => p.Id).HasColumnName("id");
                punch.HasKey(p => p.Id);
                punch.Property(p => p.DateAdded).HasColumnName("date_added");
                punch.Property(p => p.Type).HasColumnName("type");

                punch.ToTable("punch");
            });

            modelBuilder.Entity<Role>(role =>
            {
                role.Property(r => r.Id).HasColumnName("id");
                role.HasKey(r => r.Id);
                role.Property(r => r.Name).HasColumnName("name");

                role.ToTable("role");
            });

            modelBuilder.Entity<DutySchedule>(schedule =>
            {
                schedule.Property(s => s.Id).HasColumnName("id");
                schedule.HasKey(s => s.Id);
                schedule.Property(s => s.DayOfWeek).HasColumnName("day_of_week");
                schedule.Property(s => s.StartTime).HasColumnName("start_time");
                schedule.Property(s => s.EndTime).HasColumnName("end_time");

                schedule.ToTable("duty_schedule");
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