using System.ComponentModel.DataAnnotations;

namespace FoodStock.Models;

public class ItemModel
{
    public int ItemID { get; set; }

    [Required]
    public StockModel Stock { get; set; } // Reference to StockModel

    [MaxLength(100)]
    public string? ItemDescription { get; set; }

    public DateTime? SpoilDate { get; set; }

    [Required]
    public float Measure { get; set; }

    public ItemModel(int itemID, StockModel stock, string itemDescription, DateTime? spoilDate, float measure)
    {
        ItemID = itemID;
        Stock = stock;
        ItemDescription = itemDescription;
        SpoilDate = spoilDate;
        Measure = measure;
    }

    public ItemModel()
    {
        Stock = new StockModel(); // Initialize with a default value
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