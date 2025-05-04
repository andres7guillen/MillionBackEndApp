using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MillionApp.Domain.Entities;
using System.Reflection.Emit;

namespace MillionApp.Data.Context;

public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Property> Properties { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<PropertyImage> PropertyImages { get; set; }
    public DbSet<PropertyTrace> PropertyTraces { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(p => p.PropertyId);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Address).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Price).IsRequired();
            entity.Property(p => p.CodeInternal).HasMaxLength(50);
            entity.Property(p => p.Year).IsRequired();

            entity.HasOne(p => p.Owner)
                  .WithMany(o => o.Properties)
                  .HasForeignKey(p => p.OwnerId);
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(o => o.OwnerId);
            entity.Property(o => o.Name).IsRequired().HasMaxLength(100);
            entity.Property(o => o.Address).HasMaxLength(200);
            entity.Property(o => o.Photo).HasMaxLength(255);
            entity.Property(o => o.Birthday).IsRequired();
        });

        modelBuilder.Entity<PropertyImage>(entity =>
        {
            entity.HasKey(i => i.PropertyImageId);
            entity.Property(i => i.File).IsRequired().HasMaxLength(255);
            entity.Property(i => i.Enabled).IsRequired();

            entity.HasOne(i => i.Property)
                  .WithMany(p => p.PropertyImages)
                  .HasForeignKey(i => i.PropertyId);
        });

        modelBuilder.Entity<PropertyTrace>(entity =>
        {
            entity.HasKey(t => t.PropertyTraceId);
            entity.Property(t => t.DateSale).IsRequired();
            entity.Property(t => t.Name).HasMaxLength(100);
            entity.Property(t => t.Value).IsRequired();
            entity.Property(t => t.Tax).IsRequired();

            entity.HasOne(t => t.Property)
                  .WithMany(p => p.PropertyTraces)
                  .HasForeignKey(t => t.PropertyId);
        });
    }
}

