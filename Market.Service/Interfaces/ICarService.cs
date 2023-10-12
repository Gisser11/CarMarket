using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Car;

namespace Market.Service.Interfaces;

public interface ICarService
{
    Task<IBaseResponse<IEnumerable<Car>>> GetCars();

    Task<IBaseResponse<Car>> GetCar(int id);

    Task<IBaseResponse<bool>> DeleteCar(int id);

    Task<IBaseResponse<Car>> GetCarByName(string name);

    Task<IBaseResponse<CarViewModel>> CreateCar(CarViewModel carViewModel);

    Task<IBaseResponse<Car>> Edit(int id, CarViewModel carViewModel);
}