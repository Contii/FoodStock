using FoodStock.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Persistence.EFCore.Repositories;

public class GenericCrudRepositoryEFCore<T>(DbContext repository) : IGenericCrudRepositoryEFCore<T> where T : class
{
    public void Add(object table, T entity) // DbSet como object para torna-lo genérico
    {
        (table as DbSet<T>)!.Add(entity);
        repository.SaveChangesAsync();
    }

    public IEnumerable<T> ObtainAll(object table)
    {
        DbSet<T>? dbSetTable = table as DbSet<T>;
        return dbSetTable!.ToList();
    }

    public T? ObtainById(int id)
    {
        throw new NotImplementedException();
    }

    public void Remove(T entity)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }
}