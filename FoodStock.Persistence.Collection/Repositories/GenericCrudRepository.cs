using FoodStock.Model.Interfaces.Repositories;

namespace FoodStock.Persistence.Collection.Repositories;

public class CollectionsGenericCrudRepository<T>(List<T> repository) : IGenericCrudRepository<T> where T : class
{

    readonly List<T> repository = repository;

    public void Add(T entity)
    {
        if (repository.Contains(entity))
            throw new InvalidOperationException("Entity already exists in the repository");
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
        if (!repository.Contains(entity))
            throw new InvalidOperationException("Entity does not exist in the repository");
        repository.Remove(entity);
    }
}