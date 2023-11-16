using Market.DAL.Interfaces.IServices;
using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Repositories;

public class StudiaRepository : IStudiaRepository
{
    private readonly ApplicationDbContext _db;

    public StudiaRepository(ApplicationDbContext db)
    {
        _db = db;
    }


    public async Task<bool> Create(Studia entity)
    {
        await _db.Studia.AddAsync(entity);
        _db.SaveChangesAsync();
        return true;
    }

    public Task<Studia> Get(int id)
    {
        throw new NotImplementedException();
    }

    //TODO isolate studia and assortment. 
    public async Task<List<Studia>> Select()
    {
        return await _db.Studia.Include(x => x.Assortments).ToListAsync(); 
    }

    public Task<bool> Delete(Studia entity)
    {
        throw new NotImplementedException();
    }

    public Task<Studia> Update(Studia entity)
    {
        throw new NotImplementedException();
    }
}