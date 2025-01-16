using System.ComponentModel.DataAnnotations;

namespace FoodStock.Models;

public class ConsumptionModel
{
    public int ConsumptionID { get; set; }

    [Required]
    public float Quantity { get; set; }

    [Required]
    public DateTime ConsumptionDate { get; set; }

    [Required]
    public StockModel Stock { get; set; }

    [Required]
    public int StockID { get; set; }

    public ConsumptionModel(int consumptionID, float quantity, DateTime consumptionDate, StockModel stock)
    {
        ConsumptionID = consumptionID;
        Quantity = quantity;
        ConsumptionDate = consumptionDate;
        Stock = stock;
        StockID = Stock.StockID;
    }

    public ConsumptionModel()
    {
        Stock = new StockModel(); // Initialize with a default value
        Stock.Name = "name";
        Stock.Category.Name = "name";
        StockID = Stock.StockID; // Ensure StockID is set correctly
    }

    public override string ToString()
    {
        return $"[{ConsumptionID}, {Quantity}, {ConsumptionDate}, {Stock?.StockID}]";
    }

    public override bool Equals(object? obj)
    {
        if (obj is ConsumptionModel other)
        {
            return ConsumptionID == other.ConsumptionID;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return ConsumptionID.GetHashCode();
    }
}