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
    public List<ShoppingItemModel> ShoppingItens { get; set; }
    

    public ShoppingListModel(int shoppingListID, string name, DateTime shoppingDate, ShoppingStatusEnum status, List<ShoppingItemModel> shoppingItens)
    {
        ShoppingListID = shoppingListID;
        Name = name;
        ShoppingDate = shoppingDate;
        Status = status;
        ShoppingItens = shoppingItens;
    }

    public ShoppingListModel() 
    {
        ShoppingItens = new List<ShoppingItemModel>(); // A good practice For required lists, avoids null reference exceptions when adding items.
        Name = string.Empty; // For required strings, a good practice to initialize them with an empty string.
    }

    public override string ToString()
    {
        return $"[{ShoppingListID}, {ShoppingDate}, {Status}]";
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