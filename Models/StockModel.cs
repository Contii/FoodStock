using System.ComponentModel.DataAnnotations;

namespace FoodStock.Models;

public class StockModel
{
    public int StockID { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Description { get; set; }

    [Required]
    public float Quantity { get; set; }

    [Required]
    public int MinQuantity { get; set; }

    [Required]
    public int MaxQuantity { get; set; }

    [Required]
    [EnumDataType(typeof(MeasureTypeEnum))]
    public MeasureTypeEnum MeasureType { get; set; }

    public List<ItemModel> Items { get; set; } = new List<ItemModel>();

    public int? CategoryID { get; set; }
    
    public CategoryModel? Category { get; set; } = new CategoryModel();

    public StockModel(int stockID, string name, string description, float quantity, int minQuantity, int maxQuantity, List<ItemModel> items, int categoryID, CategoryModel category, MeasureTypeEnum measureType)
    {
        StockID = stockID;
        Name = name;
        Description = description;
        Quantity = quantity;
        MinQuantity = minQuantity;
        MaxQuantity = maxQuantity;
        Items = items;
        CategoryID = categoryID;
        Category = category;
        MeasureType = measureType;
    }

    public StockModel()
    {
        Name = string.Empty;
        Items = new List<ItemModel>();
        Category = new CategoryModel();
    }
    public override string ToString()
    {
        return $"[{StockID}, {Name}, {Description}, {Quantity}, {MinQuantity}, {MaxQuantity}, {MeasureType}, {Category?.CategoryID}]";
    }

    public override bool Equals(object? obj)
    {
        if (obj is StockModel other)
        {
            return StockID == other.StockID;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return StockID.GetHashCode();
    }
}