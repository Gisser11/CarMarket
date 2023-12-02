using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Repositories;

public class AssortmentRepository : IAssortmentRepository
{
    private readonly ApplicationDbContext _db;

    public AssortmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public Task<bool> Create(Assortment entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<Assortment>> Select()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(Assortment entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<Assortment>> GetAssortmentsWithKey(int id)
    {
        return _db.Assortments
            .Where(_ => _.AssortmentId == id)
            .ToListAsync();
    }
 
}