namespace FoodStock.Models.Interfaces
{
    public interface IObserver
    {
        void Update(object newState, object oldState);
    }
}