namespace FoodStock.Model.Common;

public class CategoryModel(int categoryID, string name, string description)
{
    public int? CategoryID { get; set; } = categoryID;
    public string? Name { get; set; } = name;
    public string? Description { get; set; } = description;
    
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