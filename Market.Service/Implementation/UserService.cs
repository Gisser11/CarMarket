using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Domain.ViewModels.User;
using Market.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Market.Service.Implementation;

public class UserService : IUserService
{
    //TODO Приватный ключ должен находиться в appsettings.json
    private readonly string secureKey = "this is secure key";

    #region Repo init. 

    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }   

    #endregion

    #region JWT SERVICES

    public string GenerateJwtToken(int id)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));

        var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        var header = new JwtHeader(credentials);

        var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));

        var securityToken = new JwtSecurityToken(header, payload);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public JwtSecurityToken Verify(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secureKey);
        tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            },
            out var validatedToken);

        return (JwtSecurityToken)validatedToken;
    }

    #endregion

    #region USER SERVICES

    public async Task<IBaseResponse<User>> GetById(int id)
    {
        var baseResponse = new BaseResponse<User>();
        
        try
        {
            var response = _userRepository.GetById(id);
            baseResponse.Data = response;   
            baseResponse.StatusCode = StatusCode.OK;
            baseResponse.Description = "Все успешно";
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<User>()
            {
                Description = "1",
                StatusCode = StatusCode.InternalServiceError
            };
        }
    }

    public async Task<IBaseResponse<User>> RegisterUser(UserRegisterViewModel dto)
    {
        var baseResponse = new BaseResponse<User>();

        try
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                TypeUserRole = dto.TypeUserRole == false ? "SimpleUser" : "StudiaCreator"
            };
            var response = _userRepository.Create(user);
            baseResponse.StatusCode = StatusCode.OK;
            baseResponse.Description = "Создание пользователя успешно";
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<User>()
            {
                Description = "RegUser : Произошла ошибка " + ex + "\n",
                StatusCode = StatusCode.InternalServiceError
            };
        }
    }

    public async Task<IBaseResponse<User>> LoginUser(UserLoginViewModel dto)
    {
        var baseResponse = new BaseResponse<User>();

        try
        {
            var response = _userRepository.GetByEmail(dto.Email);

            if (response == null)
            {
                baseResponse.StatusCode = StatusCode.NotFound;
                baseResponse.Description = "Не найдено записи";
                return baseResponse;
            }
            
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, response.Password))
            {
                baseResponse.StatusCode = StatusCode.NotFound;
                baseResponse.Description = "Неверные данные";
                return baseResponse;
            }

            var token = GenerateJwtToken(response.Id);
            
            baseResponse.Token = token;
            baseResponse.Description = "Успешно";
            baseResponse.StatusCode = StatusCode.OK;
            baseResponse.Data = response;
            
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<User>()
            {
                Description = "LoginUser exception : " + "\n" + ex + "\n",
                StatusCode = StatusCode.InternalServiceError 
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<User>>> SelectAll()
    {
        var baseResponse = new BaseResponse<IEnumerable<User>>();

        try
        {
            var response = _userRepository.Select();

            if (response.Result == null)
            {
                baseResponse.StatusCode = StatusCode.NotFound;
                baseResponse.Description = "Не найдено записей  ";
                return baseResponse;
            }
            
            
            baseResponse.Description = "Успешно";
            baseResponse.StatusCode = StatusCode.OK;
            baseResponse.Data = response.Result;
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<User>>()
            {
                Description = "1",
                StatusCode = StatusCode.InternalServiceError
            };
        }
    }

    public async Task<IBaseResponse<User>> GetUser(string cookiesToken)
    {
        var baseResponse = new BaseResponse<User>();
        try
        {
            var token = Verify(cookiesToken);
            var userId = int.Parse(token.Issuer);
            var response = _userRepository.GetById(userId);

            baseResponse.Description = "GetUser : Сервис выполнен";
            baseResponse.StatusCode = StatusCode.OK;
            baseResponse.Data = response;
            
            return baseResponse;
        }
        
        catch (Exception ex)
        {
            return new BaseResponse<User>()
            {
                Description = "Произошла ошибка" + ex,
                StatusCode = StatusCode.InternalServiceError
            };
        }
    }

    public async Task<IBaseResponse<User>> CheckUserEmail(string email)
    {
        var baseResponse = new BaseResponse<User>();
        try
        {
            var response = _userRepository.GetByEmail(email);
        
            if (response == null)
            {
                baseResponse.StatusCode = StatusCode.NotFound;
                baseResponse.Description = "Не найдено записей  ";
                return baseResponse;
            }

            baseResponse.StatusCode = StatusCode.OK;
            return baseResponse;

        }
        catch (Exception ex)
        {
            return new BaseResponse<User>()
            {
                Description = "CheckUserEmail : Произошла ошибка" + ex,
                StatusCode = StatusCode.InternalServiceError
            };
        }
    }

    #endregion
}