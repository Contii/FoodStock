// See https://aka.ms/new-console-template for more information
using FoodStock.Model.Common;

Console.WriteLine("Hello, World!");
 
CategoryModel category = new (1, "Grain", "Arroz");
CategoryModel category2 = new (1, "Grain", "Arroz");


Console.Write(category == category2);
