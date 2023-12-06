using Market.Domain.Entity;

namespace Market.DAL.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    User GetByEmail(string email);
    
    User GetById(int id);
}