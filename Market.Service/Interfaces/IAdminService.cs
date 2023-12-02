using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.StudiaViewModel;
using Market.Domain.ViewModels.User;

namespace Market.Service.Interfaces;

public interface IAdminService
{
    Task<IBaseResponse<User>> Edit(int id, UserViewModel model);
    
    Task<IBaseResponse<Studia>> EditStudia(int id, StudiaViewModel model);
}