using System.ComponentModel.DataAnnotations;

namespace FoodStock.Models;

public class CategoryModel
{
    public int CategoryID { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<ItemModel>? Items { get; set; }

    public CategoryModel(int categoryID, string name, string description)
    {
        CategoryID = categoryID;
        Name = name;
        Description = description;
        Items = new List<ItemModel>();
    }

    public CategoryModel()
    {
        Items = new List<ItemModel>();
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