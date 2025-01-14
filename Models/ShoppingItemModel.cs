using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodStock.Models;

public class ShoppingItemModel
{
    public int ShoppingItemID { get; set; }

    [Required]
    public float Measure { get; set; }

    [Required]
    [JsonIgnore]
    public StockModel Stock { get; set; }

    [Required]
    public int StockID { get; set; }
    
    [Required]
    [JsonIgnore]
    public ShoppingListModel ShoppingList { get; set; }

    [Required]
    public int ShoppingListID { get; set; }

    public ShoppingItemModel(int shoppingItemID, float measure, StockModel stock, ShoppingListModel shoppingList)
    {
        ShoppingItemID = shoppingItemID;
        Measure = measure;
        Stock = stock;
        StockID = stock.StockID;
        ShoppingList = shoppingList;
        ShoppingListID = shoppingList.ShoppingListID;
    }

    public ShoppingItemModel() 
    {
        Stock = new StockModel(); // Initialize with a default value
        StockID = Stock.StockID; // Ensure StockID is set correctly
        ShoppingList = new ShoppingListModel(); // Initialize with a default value  
        ShoppingListID = ShoppingList.ShoppingListID; // Ensure ShoppingListID is set correctly
    }

    public override string ToString()
    {
        return $"[{ShoppingItemID}, {Measure}, {Stock?.StockID}, {ShoppingList?.ShoppingListID}]";
    }

    public override bool Equals(object? obj)
    {
        if (obj is ShoppingItemModel other)
        {
            return other.ShoppingItemID == ShoppingItemID;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return ShoppingItemID.GetHashCode();
    }
}