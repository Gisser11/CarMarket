using Market.Domain.Entity;
using Market.Domain.Response;

namespace Market.Service.Interfaces;

public interface IAssortmentService
{
    Task<IBaseResponse<IEnumerable<Assortment>>> GetAssortmentList(int id);
}