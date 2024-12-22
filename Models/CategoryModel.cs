using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodStock.Models;

public class CategoryModel
{
    public int CategoryID { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    [JsonIgnore]
    public List<StockModel>? Stocks { get; set; }

    public CategoryModel(int categoryID, string name, string description)
    {
        CategoryID = categoryID;
        Name = name;
        Description = description;
        Stocks = new List<StockModel>();
    }

    public CategoryModel() 
    {
        Stocks = new List<StockModel>(); // A good practice For required lists, avoids null reference exceptions when adding items.
        Name = string.Empty; // For required strings, a good practice to initialize them with an empty string.
    }

    public override string ToString()
    {
        return $"[{CategoryID}, {Name}, {Description}]";
    }

    public override bool Equals(object? obj)
    {
        if (obj is CategoryModel other)
        {
            return other.CategoryID == CategoryID;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return CategoryID.GetHashCode();
    }
}