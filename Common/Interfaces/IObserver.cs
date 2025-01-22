namespace FoodStock.Common.Interfaces
{
    public interface IObserver
    {
        void Update(object newState, float oldQuantity);
    }
}