using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodStock.Models;

public class ShoppingItemModel
{
    public int ShoppingItemID { get; set; }

    [Required]
    public float Measure { get; set; }

    [Required]
    public StockModel Stock { get; set; }

    [Required]
    public int StockID { get; set; }
    

    public ShoppingItemModel(int shoppingItemID, StockModel stock, float measure)
    {
        ShoppingItemID = shoppingItemID;
        Stock = stock;
        StockID = stock.StockID;
        Measure = measure;
    }

    public ShoppingItemModel() 
    {
        Stock = new StockModel(); // Initialize with a default value
        StockID = Stock.StockID; // Ensure StockID is set correctly
    }

    public override string ToString()
    {
        return $"[{ShoppingItemID}, {Name}, {Description}]";
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