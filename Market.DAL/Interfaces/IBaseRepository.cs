using Market.Domain.Entity;

namespace Market.DAL.Interfaces;

// При наследовании
// будем указывать, какой тип будем передавть, и в зависимости какой объект,
// будет своя логика
public interface IBaseRepository<T>
{
    Task<bool> Create(T entity);

    Task<Car> Get(int id);

    Task<List<Car>> Select();

    Task<bool> Delete(T entity);
    
    
}