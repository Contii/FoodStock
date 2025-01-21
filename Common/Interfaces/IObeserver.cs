namespace FoodStock.Common.Interfaces
{
    public interface IObserver
    {
        void Update(object newState, object oldState);
    }
}