using Market.DAL.Interfaces.IServices;
using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Repositories;

public class StudiaRepository : IStudiaRepository
{
    #region _dbContext
    private readonly ApplicationDbContext _db;
    
    public StudiaRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    #endregion

    #region StudiaMethods

    public async Task<bool> Create(Studia entity)
    {
        await _db.Studia.AddAsync(entity);
        await _db.SaveChangesAsync();
        return true;
    }
    
    public async Task<List<Studia>> Select()
    {
        return await _db.Studia.Include(x => x.Assortments).ToListAsync(); 
    }
    
    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(Studia entity)
    {
        throw new NotImplementedException();
    }

    public Studia? GetById(int id)
    {
        return _db.Studia.FirstOrDefault(u => u.Id == id);
    }

    #endregion
}