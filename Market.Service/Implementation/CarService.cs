using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Domain.ViewModels.Car;
using Market.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Market.Service.Implementation;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;

    public CarService(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }
    
    public async Task<IBaseResponse<IEnumerable<Car>>> GetCars()
    {
        var baseResponse = new BaseResponse<IEnumerable<Car>>();
        try
        {
            var cars = await _carRepository.Select();
            if (cars.Count == 0)
            {
                baseResponse.Description = "Найдено 0 элементов";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }

            baseResponse.Data = cars;
            baseResponse.StatusCode = StatusCode.OK;
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<Car>>()
            {
                Description = $"[GetCars] : {ex.Message}"
            };
        }
    }

    public async Task<IBaseResponse<Car>> GetCar(int id)
    {
        var baseResponse = new BaseResponse<Car>();

        try
        {
            var car = await _carRepository.Get(id);

            if (car == null)
            {
                baseResponse.Description = "Ошибка, запрос не найден";
                baseResponse.StatusCode = StatusCode.NotFound;
                return baseResponse;
            }

            baseResponse.Data = car;
            baseResponse.StatusCode = StatusCode.OK;
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<Car>()
            {
                Description = $"[GetCar] : {ex.Message}",
                StatusCode = StatusCode.InternalServiceError
            };
        }
    }

    public async Task<IBaseResponse<bool>> DeleteCar(int id)
    {
        var baseResponse = new BaseResponse<bool>();

        try
        {
            var car = await _carRepository.Get(id);
            if (car == null)
            {
                baseResponse.Description = "Ошибка, запрос не найден";
                baseResponse.StatusCode = StatusCode.NotFound;
                return baseResponse;
            }

            await _carRepository.Delete(car);
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[Delete] : {ex.Message}",
                StatusCode = StatusCode.InternalServiceError
            };
        }
    }

    public async Task<IBaseResponse<Car>> GetCarByName(string name)
    {
        var baseResponse = new BaseResponse<Car>();
        try
        {
            var car = await _carRepository.GetByName(name);
            if (car == null)
            {
                baseResponse.Description = "Найдено 0 элементов";
                baseResponse.StatusCode = StatusCode.NotFound;
                return baseResponse;
            }

            baseResponse.Data = car;
            baseResponse.StatusCode = StatusCode.OK;
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<Car>()
            {
                Description = $"[GetCarByName] : {ex.Message}",
                StatusCode = StatusCode.InternalServiceError
            };
        }
    }

    public async Task<IBaseResponse<CarViewModel>> CreateCar(CarViewModel carViewModel)
    {
        var baseResponse = new BaseResponse<CarViewModel>();
        try
        {
            var car = new Car()
            {
                Description = carViewModel.Description,
                DataCreate = DateTime.Now,
                Speed = carViewModel.Speed,
                Model = carViewModel.Model,
                Price = carViewModel.Price,
                Name = carViewModel.Name,
                TypeCar = (TypeCar)Convert.ToInt32(carViewModel.TypeCar)
            };

            await _carRepository.Create(car); 
        }
        catch (Exception ex)
        {
            return new BaseResponse<CarViewModel>()
            {
                Description = $"[CreateCar] : {ex.Message}",
                StatusCode = StatusCode.InternalServiceError
            };
        }
        return baseResponse;
    }

    public async Task<IBaseResponse<Car>> Edit(int id, CarViewModel carViewModel)
    {
        var baseResponse = new BaseResponse<Car>();
       
        try
        {
            var car = await _carRepository.Get(id);

            if (car == null)
            {
                baseResponse.StatusCode = StatusCode.NotFound;
                baseResponse.Description = "Car Not Found";
                return baseResponse;
            }

            car.Description = carViewModel.Description;
            car.Model = carViewModel.Model;
            car.Price = carViewModel.Price;
            car.Speed = carViewModel.Speed;
            car.DataCreate = carViewModel.DataCreate;
            car.Name = carViewModel.Name;
            //TypeCar
            await _carRepository.Update(car);

            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<Car>()
            {
                Description = $"[Edit] : {ex.Message}",
                StatusCode = StatusCode.InternalServiceError
            };
        }
    }
}