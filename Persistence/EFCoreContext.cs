using FoodStock.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Persistence;

public class EFCoreContext : DbContext
{
    public DbSet<CategoryModel>  Categories { get; set; }
    public DbSet<ItemModel> Items { get; set; }

    // public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options)
    public EFCoreContext( ) // Uncomment this and comment above line to create/update migration files.
    {    
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options.UseSqlite("Data Source=../utfpr.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
        .Entity<CategoryModel>( // Define the CategoryModel entity.
            eb =>
            {

                eb.HasKey(pk => pk.CategoryID); // CategoryID is the primary key in the database.
                eb.Property(p => p.Name).IsRequired(); // Name is required in the database.
                eb.Property(p => p.Description).HasMaxLength(100);
                eb.HasMany(p => p.Stocks) // A category can have many stocks in the database.
                    // .WithOne(p => p.Category) // An stock belongs to a category in the database.
                    //.HasForeignKey(p => p.CategoryID) // The foreign key of the stock is the CategoryID.
                    //.OnDelete(DeleteBehavior.SetNull); // If a category is deleted, the stocks will have the CategoryID set to null.
            });
        // modelBuilder.Entity<ItemModel>( // Define the ItemModel entity.
        //     eb =>
        //     {
        //         eb.HasKey(pk => pk.ItemID);
        //         eb.Property(p => p.Name).IsRequired();
        //         eb.Property(p => p.Measure).IsRequired();
        //         eb.Property(p => p.MeasureType).IsRequired();
        //         eb.HasOne(p => p.Category)
        //             .WithMany(p => p.Items)
        //             .HasForeignKey(p => p.CategoryID);
        //     });

        base.OnModelCreating(modelBuilder);
    }
}