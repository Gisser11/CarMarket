using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Repositories;

public class CarRepository : ICarRepository
{
    private readonly ApplicationDbContext _db;

    public CarRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Create(Car entity)
    {
        await _db.Car.AddAsync(entity);
        _db.SaveChangesAsync();
        return true;
    }

    public async Task<Car> Get(int id)
    {
        return await _db.Car.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Car>> Select()
    {
        return await _db.Car.ToListAsync();
    }

    public async Task<bool> Delete(Car entity)
    {
        _db.Car.Remove(entity); // изменения без сохранений 
        _db.SaveChangesAsync(); // сохраняем в бд
        return true;
    }

    public async Task<Car> Update(Car entity)
    {
        _db.Car.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<Car> GetByName(string name)
    {
        return await _db.Car.FirstOrDefaultAsync(x => x.Name == name);
    }

    public IQueryable<Car> GetAll()
    {
        throw new NotImplementedException();
    }
}