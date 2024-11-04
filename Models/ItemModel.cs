using System.ComponentModel.DataAnnotations;

namespace FoodStock.Models;

public class ItemModel
{
    public int ItemID { get; set; }
    [Required]
    public string Name { get; set; }
    [MaxLength(100)]
    public string Description { get; set; }
    public DateTime SpoilDate { get; set; }
    [Required]
    public float Measure { get; set; }
    [Required] 
    public MeasureTypeEnum MeasureType { get; set; }
    public int CategoryID { get; set; }
    public CategoryModel Category { get; set; }

    public ItemModel(int itemID, string name, string description, DateTime spoilDate, float measure, MeasureTypeEnum measureType, int categoryID)
    {
        ItemID = itemID;
        Name = name;
        Description = description;
        SpoilDate = spoilDate;
        Measure = measure;
        MeasureType = measureType;
        CategoryID = categoryID;
        Category = new CategoryModel();
    }

    public ItemModel()
    {
        Name = string.Empty;
        Description = string.Empty;
        Category = new CategoryModel();
    }

    public override string ToString()
    {
        return $"[{ItemID}, {Name}, {Description}, {SpoilDate}, {Measure}, {MeasureType}, {CategoryID}]";
    }

    public override bool Equals(object? obj)
    {
        if (obj is ItemModel other)
        {
            return other.ItemID == ItemID;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return ItemID.GetHashCode();
    }
}