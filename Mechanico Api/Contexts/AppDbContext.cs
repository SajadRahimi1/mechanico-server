using Mechanico_Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mechanico_Api.Contexts;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Entities.User>().HasKey(_ => _.Id);
        modelBuilder.Entity<Entities.Mechanic>().HasKey(_ => _.Id);
        modelBuilder.Entity<Entities.Visited>().HasKey(_ => _.Id);
        modelBuilder.Entity<Entities.Category>().HasKey(_ => _.Id);
        modelBuilder.Entity<Entities.Comment>().HasKey(_ => _.Id);
        modelBuilder.Entity<Entities.SmsCode>().HasKey(_ => _.Id);

        modelBuilder.Entity<Entities.Comment>().HasOne<User>(c => c.User).WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId);
        modelBuilder.Entity<Entities.Comment>().HasOne<Mechanic>(c => c.Mechanic).WithMany(m => m.Comments)
            .HasForeignKey(c => c.MechanicId);
        
        modelBuilder.Entity<Entities.Visited>().HasOne<User>(v => v.User).WithMany(u => u.Visiteds)
            .HasForeignKey(v => v.UserId);
        modelBuilder.Entity<Entities.Visited>().HasOne<Mechanic>(v => v.Mechanic).WithMany(m => m.Visiteds)
            .HasForeignKey(v => v.MechanicId);

        modelBuilder.Entity<Mechanic>().HasMany<Category>(m => m.Categories);
    }

    public override int SaveChanges()
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries()
                     .Where(e => e.State is EntityState.Added or EntityState.Modified))
        {
            entry.Property("UpdatedAt").CurrentValue = now;

            if (entry.State == EntityState.Added)
            {
                entry.Property("CreatedAt").CurrentValue = now;
            }
        }

        return base.SaveChanges();
    }

    public  DbSet<User> Users { get;set; }
    public  DbSet<Mechanic> Mechanics { get;set; }
    public  DbSet<Comment> Comments { get;set; }
    public  DbSet<Visited> Visiteds { get;set; }
    public  DbSet<Category> Categories { get;set; }
    public  DbSet<SmsCode> SmsCodes { get;set; }
}