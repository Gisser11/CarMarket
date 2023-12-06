using System.IdentityModel.Tokens.Jwt;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.User;

namespace Market.Service.Interfaces;

public interface IUserService
{
    #region JWT SERVICE

    public string GenerateJwtToken(int id);
    
    public JwtSecurityToken Verify(string token);

    #endregion

    #region USER SERVICE

    Task<IBaseResponse<User>> GetById(int id);

    Task<IBaseResponse<User>> RegisterUser(UserRegisterViewModel dto);

    Task<IBaseResponse<User>> LoginUser(UserLoginViewModel dto);

    Task<IBaseResponse<IEnumerable<User>>> SelectAll();
    
    Task<IBaseResponse<User>> GetUser(string cookiesToken);

    Task<IBaseResponse<User>> CheckUserEmail(string email);

    #endregion
}