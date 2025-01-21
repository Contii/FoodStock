using FoodStock.Models.Interfaces;

namespace FoodStock.Models.Observers
{
    public class StockObserver : IObserver
    {
        public void Update(object newState, object oldState)
        {
            if (newState is StockModel newStock && oldState is dynamic oldStock)
            {
                if (newStock.Quantity < oldStock.Quantity) // Verify if the quantity has decreased
                {
                    Console.WriteLine($"Produto consumido. Quantidade anterior: {oldStock.Quantity}, Quantidade atual: {newStock.Quantity}");
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