using Aromato.Domain;
using Aromato.Domain.CredentialAgg;
using Aromato.Domain.EmployeeAgg;
using Aromato.Domain.InventoryAgg;
using Aromato.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace Aromato.Infrastructure.PostgreSQL
{
    public sealed class PostgresUnitOfWork : DbContext, IUnitOfWork
    {
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }

        public PostgresUnitOfWork()
        {
        }

        public PostgresUnitOfWork(DbContextOptions<PostgresUnitOfWork> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Credential>(cred =>
            {
                cred.Property(e => e.Id).HasColumnName("id");
                cred.HasKey(e => e.Id);

                cred.Property(e => e.Username).HasColumnName("username");
                cred.Property(e => e.Password).HasColumnName("password");

                cred.HasOne(e => e.Employee).WithMany().HasForeignKey("employee_id");

                cred.ForNpgsqlUseXminAsConcurrencyToken();

                cred.ToTable("credential");
            });

            modelBuilder.Entity<Role>(role =>
            {
                role.Property(e => e.Id).HasColumnName("id");
                role.HasKey(e => e.Id);

                role.Property(i => i.Name).HasColumnName("name");
                role.Property(i => i.Description).HasColumnName("description");
                role.HasMany(i => i.Permissions).WithOne().HasForeignKey("role_id");
                role.HasMany(i => i.Employees).WithOne().HasForeignKey("role_id");

                role.ForNpgsqlUseXminAsConcurrencyToken();

                role.ToTable("role");
            });

            modelBuilder.Entity<Permission>(perm =>
            {
                perm.Property(e => e.Id).HasColumnName("id");
                perm.HasKey(e => e.Id);

                perm.Property(i => i.Name).HasColumnName("name");
                perm.Property(i => i.Description).HasColumnName("description");
                perm.HasMany(i => i.Roles).WithOne().HasForeignKey("permission_id");

                perm.ForNpgsqlUseXminAsConcurrencyToken();

                perm.ToTable("permission");
            });

            modelBuilder.Entity<RolePermission>(rolePerm =>
            {
                rolePerm.Property<long>("permission_id").UseNpgsqlSerialColumn();
                rolePerm.Property<long>("role_id").UseNpgsqlSerialColumn();
                rolePerm.HasKey("role_id", "permission_id");

                rolePerm.HasOne(role => role.Role)
                    .WithMany(e => e.Permissions)
                    .HasForeignKey("role_id");

                rolePerm.HasOne(perm => perm.Permission)
                    .WithMany(e => e.Roles)
                    .HasForeignKey("permission_id");

                rolePerm.ForNpgsqlUseXminAsConcurrencyToken();

                rolePerm.ToTable("role_permission");
            });

            modelBuilder.Entity<Employee>(employee =>
            {
                employee.Property(e => e.Id).HasColumnName("id");
                employee.HasKey(e => e.Id);
                employee.HasAlternateKey(e => e.UniqueId);
                employee.Property(e => e.FirstName).HasColumnName("first_name");
                employee.Property(e => e.MiddleName).HasColumnName("middle_name");
                employee.Property(e => e.LastName).HasColumnName("last_name");
                employee.Property(e => e.Gender).HasColumnName("gender");
                employee.Property(e => e.ContactNo).HasColumnName("contact_number");
                employee.Property(e => e.Email).HasColumnName("email");
                employee.Property(e => e.UniqueId).HasColumnName("unique_id");
                employee.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
                employee.HasMany(e => e.Punches).WithOne().HasForeignKey("employee_id");
                employee.HasMany(e => e.Roles).WithOne().HasForeignKey("employee_id");

                employee.ForNpgsqlUseXminAsConcurrencyToken();

                employee.ToTable("employee");
            });

            modelBuilder.Entity<EmployeeRole>(empRole =>
            {
                empRole.Property<long>("employee_id").UseNpgsqlSerialColumn();
                empRole.Property<long>("role_id").UseNpgsqlSerialColumn();

                empRole.HasKey("employee_id", "role_id");

                empRole.HasOne(role => role.Role)
                    .WithMany(e => e.Employees)
                    .HasForeignKey("role_id");

                empRole.HasOne(perm => perm.Employee)
                    .WithMany(e => e.Roles)
                    .HasForeignKey("employee_id");

                empRole.ForNpgsqlUseXminAsConcurrencyToken();

                empRole.ToTable("employee_role");
            });

            modelBuilder.Entity<Inventory>(inventory =>
            {
                inventory.Property(i => i.Id).HasColumnName("id");
                inventory.HasKey(i => i.Id);
                inventory.Property(i => i.Name).HasColumnName("name");
                inventory.Property(i => i.Description).HasColumnName("description");
                inventory.HasMany(i => i.Items).WithOne().HasForeignKey("inventory_id");

                inventory.ForNpgsqlUseXminAsConcurrencyToken();

                inventory.ToTable("inventory");
            });

            modelBuilder.Entity<Item>(item =>
            {
                item.Property(i => i.Id).HasColumnName("id");
                item.HasKey(i => i.Id);
                item.Property(i => i.Name).HasColumnName("name");
                item.Property(i => i.Description).HasColumnName("description");

                item.ForNpgsqlUseXminAsConcurrencyToken();

                item.ToTable("item");
            });

            modelBuilder.Entity<InventoryItem>(item =>
            {
                item.Property(i => i.Id).HasColumnName("id");
                item.HasKey(i => i.Id);
                item.HasAlternateKey(i => i.UniqueId);
                item.Property(i => i.UniqueId).HasColumnName("unique_id");
                item.Property(i => i.DateAdded).HasColumnName("date_added");
                item.Property(i => i.LastUpdated).HasColumnName("last_updated");
                item.Property(i => i.Status).HasColumnName("status");

                item.HasOne(i => i.Item).WithMany().HasForeignKey("item_id");

                item.ForNpgsqlUseXminAsConcurrencyToken();

                item.ToTable("inventory_item");
            });

            modelBuilder.Entity<Punch>(punch =>
            {
                punch.Property(p => p.Id).HasColumnName("id");
                punch.HasKey(p => p.Id);
                punch.Property(p => p.DateAdded).HasColumnName("date_added");
                punch.Property(p => p.Type).HasColumnName("type");

                punch.ForNpgsqlUseXminAsConcurrencyToken();

                punch.ToTable("punch");
            });

            modelBuilder.Entity<DutySchedule>(schedule =>
            {
                schedule.Property(s => s.Id).HasColumnName("id");
                schedule.HasKey(s => s.Id);
                schedule.Property(s => s.DayOfWeek).HasColumnName("day_of_week");
                schedule.Property(s => s.StartTime).HasColumnName("start_time");
                schedule.Property(s => s.EndTime).HasColumnName("end_time");

                schedule.ForNpgsqlUseXminAsConcurrencyToken();

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