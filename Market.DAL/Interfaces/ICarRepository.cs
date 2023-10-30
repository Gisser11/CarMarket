using Market.Domain.Entity;

namespace Market.DAL.Interfaces;

public interface ICarRepository : IBaseRepository<Car>
{
    Task<Car> GetByName(string name);
}