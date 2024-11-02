using FoodStock.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Persistence;

public class EFCoreContext : DbContext
{
    public DbSet<CategoryModel>  Categories { get; set; }

    // public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options)
    public EFCoreContext( ) // Uncomment this and comment above line to create migration files.
    {    
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options.UseSqlite("Data Source=../utfpr.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
        .Entity<CategoryModel>(
            eb =>
            {
                eb.HasKey(pk => pk.CategoryID);
            });

        base.OnModelCreating(modelBuilder);
    }
}