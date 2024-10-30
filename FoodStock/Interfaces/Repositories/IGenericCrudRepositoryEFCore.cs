namespace FoodStock.Model.Interfaces.Repositories;

public interface IGenericCrudRepositoryEFCore<T> where T : class // Garantia de que seja uma classe
{
    void Add(object table,T entity); // DBset como object para torna-lo genérico

    T? ObtainById(int id);

    IEnumerable<T> ObtainAll(object table);

    void Update(T entity);

    void Remove(T entity);
}