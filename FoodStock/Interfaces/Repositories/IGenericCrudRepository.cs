namespace FoodStock.Model.Interfaces.Repositories;

public interface IGenericCrudRepository<T> where T : class // Garantia de que seja uma classe
{
    // Adiciona uma nova entidade ao repositório
    void Add(T entity);

    // Obtém uma entidade do repositório pelo seu ID
    T? ObtainById(int id);

    // Obtém todas as entidades do repositório
    IEnumerable<T> ObtainAll();

    // Atualiza uma entidade existente no repositório
    void Update(T entity);
}