using Market.Domain.Entity;

namespace Market.DAL.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> SimpleUser(int id);

    Task<List<User>> SelectUsers();

    User GetByEmail(string email);
    User GetById(int id);
}