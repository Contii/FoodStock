using System.ComponentModel.DataAnnotations;

namespace FoodStock.Models;

public class ItemModel
{
    public int ItemID { get; set; }

    [MaxLength(100)]
    public string? ItemDescription { get; set; }

    public DateTime? SpoilDate { get; set; }

    [Required]
    public float Measure { get; set; }

    [Required]
    public StockModel Stock { get; set; } // Reference to StockModel

    [Required]
    public int StockID { get; set; } // Foreign key to StockModel

    public ItemModel(int itemID, StockModel stock, string itemDescription, DateTime? spoilDate, float measure)
    {
        ItemID = itemID;
        Stock = stock;
        StockID = stock.StockID;
        ItemDescription = itemDescription;
        SpoilDate = spoilDate;
        Measure = measure;
    }

    public ItemModel()
    {
        Stock = new StockModel(); // Initialize with a default value
        StockID = Stock.StockID; // Ensure StockID is set correctly
    }

    public override string ToString()
    {
        return $"[{ItemID}, {ItemDescription}, {SpoilDate}, {Measure}, {Stock.Name}, {Stock.Description}, {Stock.MeasureType}]";
    }

    public override bool Equals(object? obj)
    {
        if (obj is ItemModel other)
        {
            return ItemID == other.ItemID;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return ItemID.GetHashCode();
    }
}