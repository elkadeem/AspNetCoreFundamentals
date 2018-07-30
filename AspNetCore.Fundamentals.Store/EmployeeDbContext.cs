using AspNetCore.Fundamentals.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace AspNetCore.Fundamentals.Store
{
    public class EmployeeDbContext : DbContext
    {        
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; private set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_configuration["EmployeeConnetionString"], c => c.EnableRetryOnFailure(2));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Employee>()
                .ToTable("Employees", "HR");
            entity.Property(c => c.IdNo)
                .IsRequired().HasMaxLength(10);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(c => c.IdNo)
                .IsUnique().HasName("IX_Employee_UniqueIdNo");
            var addressEntity = entity.OwnsOne(c => c.Address);
            addressEntity.Property(c => c.City).IsRequired().HasMaxLength(100);
            addressEntity.Property(c => c.Line1).IsRequired().HasMaxLength(200);
            addressEntity.Property(c => c.Line2).HasMaxLength(200);
        }
    }
}
