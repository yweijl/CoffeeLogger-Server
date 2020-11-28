using Core.Enums;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Infrastructure.Interfaces;

namespace Infrastructure
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<Flavor> Flavors { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<RecordFlavor> RecordFlavors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Coffee>()
                .Property(e => e.CoffeeType)
                .HasConversion(
                    x => x.ToString(),
                    x => (CoffeeType)Enum.Parse(typeof(CoffeeType), x));

            modelBuilder.Entity<Flavor>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder
                .Entity<Record>()
                .HasMany(r => r.Flavors)
                .WithMany(r => r.Records)
                .UsingEntity<RecordFlavor>(
                rf => rf.HasOne(x => x.Flavor).WithMany().HasForeignKey(x => x.FlavorId),
                rf => rf.HasOne(x => x.Record).WithMany().HasForeignKey(x => x.RecordId))
                .HasKey(rf => rf.Id);

            modelBuilder
                .Entity<RecordFlavor>()
                .HasIndex(rf => new { rf.RecordId, rf.FlavorId })
                .IsUnique();
        }
    }
}
