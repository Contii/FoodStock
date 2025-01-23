using FoodStock.Common.Interfaces;
using FoodStock.Models;
using Microsoft.EntityFrameworkCore;

// This is an example of the Concrete Observer class for the StockModel class. The Observer itself is the IObserver interface in the Interfaces folder.
// The Concrete Subject class is the modification in StockModel class to implement the ISubject interface. The Subject itself is the ISubject interface in the Interfaces folder.

namespace FoodStock.Persistence.Observers
{
    public class StockObserver : IObserver
    {
        private readonly EFCoreContext _context;

        public StockObserver(EFCoreContext context)
        {
            _context = context;
        }

        public void Update(object newState, float oldQuantity)
        {
            if (newState is StockModel newStock)
            {
                if (newStock.Quantity < oldQuantity)
                {
                    Console.WriteLine($"Produto consumido. Quantidade anterior: {oldQuantity}, Quantidade atual: {newStock.Quantity}");

                    var consumption = new ConsumptionModel // Create a new ConsumptionModel
                    {
                        Quantity = oldQuantity - newStock.Quantity,
                        ConsumptionDate = DateTime.Today,
                        Stock = newStock,
                        StockID = newStock.StockID
                    };

                    // Ensure the existing stock is tracked correctly
                    _context.Entry(newStock).State = EntityState.Unchanged;

                    _context.Consumptions.Add(consumption);
                    _context.SaveChanges();

                }

                if (newStock.Quantity < newStock.MinQuantity) // Verify if the quantity is below the minimum
                {
                    Console.WriteLine($"Stock {newStock.Name} está abaixo do mínimo. Quantidade atual: {newStock.Quantity}, MinQuantity: {newStock.MinQuantity}");
                }

                if (newStock.Quantity > newStock.MaxQuantity) // Verify if the quantity is above the maximum
                {
                    Console.WriteLine($"Stock {newStock.Name} está acima do máximo. Quantidade atual: {newStock.Quantity}, MaxQuantity: {newStock.MaxQuantity}");
                }
            }
        }
    }
}