using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Domain.ViewModels.User;
using Market.Service.Interfaces;

namespace Market.Service.Implementation;

public class AdminService :IAdminService
{
    private readonly IUserRepository _userRepository;

    public AdminService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IBaseResponse<User>> Edit(int id, UserViewModel model)
    {
        var baseResponse = new BaseResponse<User>();
        
        try
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                baseResponse.StatusCode = StatusCode.NotFound;
                baseResponse.Description = "User not found";
                return baseResponse;
            }

            // Update user properties from the model
            user.Name = model.Name;
            user.Email = model.Email;
            user.TypeUserRole = model.TypeUserRole;

            await _userRepository.Update(user);

            baseResponse.StatusCode = StatusCode.OK;
            baseResponse.Data = user; 

            return baseResponse;
        }
        catch (Exception ex)
        {
            // Log the exception for debugging
            // logger.LogError(ex, "Error occurred while editing user.");

            return new BaseResponse<User>()
            {
                Description = $"[Edit] : {ex.Message}",
                StatusCode = StatusCode.InternalServiceError
            };
        }
    }
}