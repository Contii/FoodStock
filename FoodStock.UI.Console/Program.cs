// See https://aka.ms/new-console-template for more information
using FoodStock.Model.Common;
using FoodStock.Persistence.Collection.Repositories;

Console.WriteLine("Hello, World!");
 
var repositoryCollection = new CollectionsGenericCrudRepository<CategoryModel>([]); // [] ao invés de new List<CategoryModel>()
repositoryCollection.Add(new CategoryModel ( 1, "Grãos", "Arroz"));

try
{
    repositoryCollection.Add(new CategoryModel ( 1, "Grãos", "Arroz"));
}
catch (InvalidOperationException e)
{
    Console.WriteLine(e.Message);
}


var categories = repositoryCollection.ObtainAll();
foreach (var category in categories)
{
    Console.WriteLine(category);
}