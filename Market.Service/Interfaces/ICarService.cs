using Market.Domain.Entity;
using Market.Domain.Response;

namespace Market.Service.Interfaces;

public interface ICarService
{
    Task<IBaseResponse<IEnumerable<Car>>> GetCars();

    Task<IBaseResponse<Car>> GetCar(int id);

    Task<IBaseResponse<bool>> DeleteCar(int id);

    Task<IBaseResponse<Car>> GetCarByName(string name);
}