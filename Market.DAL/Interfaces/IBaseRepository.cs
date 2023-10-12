using Market.Domain.Entity;

namespace Market.DAL.Interfaces;

// При наследовании
// будем указывать, какой тип будем передавть, и в зависимости какой объект,
// будет своя логика
public interface IBaseRepository<T>
{
    Task<bool> Create(T entity);

    Task<Car> Get(int id);

    IQueryable<T> GetAll();

    Task<bool> Delete(T entity);

    Task<T> Update(T entity);

}