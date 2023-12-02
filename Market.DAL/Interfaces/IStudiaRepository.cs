using Market.Domain.Entity;

namespace Market.DAL.Interfaces.IServices;

public interface IStudiaRepository : IBaseRepository<Studia>
{
    Studia? GetById(int id);
}