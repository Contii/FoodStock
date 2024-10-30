// See https://aka.ms/new-console-template for more information

using FoodStock.Model.Common;
using FoodStock.Persistence.EFCore.Database.Contexts;
using FoodStock.Persistence.EFCore.Repositories;

var efDbContext = new SqliteEFCoreContext();
var repositoryEF = new GenericCrudRepositoryEFCore<CategoryModel>(new SqliteEFCoreContext());
repositoryEF.Add(efDbContext.Categories, new CategoryModel(1, "grains", "Rice"));
repositoryEF.Add(efDbContext.Categories, new CategoryModel(2, "grains", "Rice"));

foreach (var category in repositoryEF.ObtainAll(efDbContext.Categories))
{
    Console.Write(category);
}