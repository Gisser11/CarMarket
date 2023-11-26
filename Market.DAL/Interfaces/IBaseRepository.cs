namespace Market.DAL.Interfaces;

// При наследовании
// будем указывать, какой тип будем передавть, и в зависимости какой объект,
// будет своя логика
public interface IBaseRepository<T>
{
    Task<bool> Create(T entity);

    Task<T> Get(int id);

    Task<List<T>> Select();

    Task<bool> Delete(int id);

    Task<T> Update(T entity);
}