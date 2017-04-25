using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Aromato.Infrastructure;
using Aromato.Domain.Enumeration;

namespace Aromato.Migrations
{
    [DbContext(typeof(AromatoContext))]
    partial class AromatoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Aromato.Domain.Aggregate.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContactNo");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Aromato.Domain.Aggregate.Inventory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("Aromato.Domain.Entity.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<Guid?>("InventoryId");

                    b.Property<string>("Name");

                    b.Property<int>("Status");

                    b.Property<string>("UniqueId");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("Aromato.Domain.Entity.Item", b =>
                {
                    b.HasOne("Aromato.Domain.Aggregate.Inventory")
                        .WithMany("Items")
                        .HasForeignKey("InventoryId");
                });
        }
    }
}
