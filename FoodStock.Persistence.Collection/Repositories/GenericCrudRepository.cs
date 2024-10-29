using FoodStock.Model.Interfaces.Repositories;

namespace FoodStock.Persistence.Collection.Repositories;

public class CollectionsGenericCrudRepository<T>(List<T> repository) : IGenericCrudRepository<T> where T : class
{

    readonly List<T> repository = repository;

    public void Add(T entity)
    {
        repository.Add(entity);
    }

    public IEnumerable<T> ObtainAll()
    {
        return repository;
    }

    public T? ObtainById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(T entity)
    {
        repository.Remove(entity);
    }
}