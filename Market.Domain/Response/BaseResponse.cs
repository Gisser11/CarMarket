using Market.Domain.Enum;

namespace Market.Domain.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    public string Description { get; set; }

    public StatusCode StatusCode { get; set; }

    public T Data { get; set; }
    
    public string? Token { get; set; } // gpt сказал, что это нормально
}

public interface IBaseResponse<T>
{
    StatusCode StatusCode { get; }
    T Data { get; }

    string Token { get; }
    
    string Description { get; }
}