using FoodStock.Model.Common;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Persistence.EFCore.Database.Contexts;

public class SqliteEFCoreContext : DbContext
{
    // public SqliteEFCoreContext(DbContextOptions<SqliteEFCoreContext> options) : base(options)
    // {}

    public DbSet<CategoryModel> Categories { get; set; } // Representação da tabela Categories em memória

    // public string DbPath { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source=G:/01 Arquivos Joao/2.3 Repositories/00 Contii/FoodStock/FoodStock.Persistence.EFCore/utfpr.db"); // Conexão com o banco de dados SQLite
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryModel>().HasKey(c => c.CategoryID); // Define a chave primária da tabela Categories
    }

}
