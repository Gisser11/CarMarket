using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Create(User entity)
    {
        await _db.User.AddAsync(entity);
        _db.SaveChangesAsync();
        return true;
    }

    public Task<User> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> Select()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> Update(User entity)
    {
        throw new NotImplementedException();
    }


    public Task<User> SimpleUser(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> SelectUsers()
    {
        return await _db.User.ToListAsync();
    }

    public User GetByEmail(string email)
    {
        return _db.User.FirstOrDefault(u => u.Email == email);
    }

    public User GetById(int id)
    {
        return _db.User.FirstOrDefault(u => u.Id == id);
    }
}