using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodStock.Models;

public class ShoppingListModel
{
    public int ShoppingListID { get; set; }

    [Required]
    public string Name { get; set; }

    public DateTime? ShoppingDate { get; set; }

    [Required]
    public ShoppingStatusEnum Status { get; set; }

    [Required]
    [JsonIgnore]
    public List<ShoppingItem>? ShoppingItens { get; set; }
    

    public ShoppingListModel(int shoppingListID, string name, string description, DateTime shoppingDate, ShoppingStatusEnum status)
    {
        ShoppingListID = shoppingListID;
        Name = name;
        Description = description;
        ShoppingDate = shoppingDate;
        Status = status;
        ShoppingItens = new List<ShoppingItem>();
    }

    public ShoppingListModel() 
    {
        ShoppingItens = new List<ShoppingItem>(); // A good practice For required lists, avoids null reference exceptions when adding items.
        Name = string.Empty; // For required strings, a good practice to initialize them with an empty string.
    }

    public override string ToString()
    {
        return $"[{ShoppingListID}, {Name}, {Description}]";
    }

    public override bool Equals(object? obj)
    {
        if (obj is ShoppingListModel other)
        {
            return other.ShoppingListID == ShoppingListID;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return ShoppingListID.GetHashCode();
    }
}