using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Service.Interfaces;

namespace Market.Service.Implementation;

public class AssortmentService: IAssortmentService
{
    private readonly IAssortmentRepository _assortmentRepository;

    public AssortmentService(IAssortmentRepository assortmentRepository)
    {
        _assortmentRepository = assortmentRepository;
    }

    public async Task<IBaseResponse<IEnumerable<Assortment>>> GetAssortmentList(int id)
    {
        var baseResponse = new BaseResponse<IEnumerable<Assortment>>();

        try
        {
            var response = await _assortmentRepository.GetAssortmentsWithKey(id);
            baseResponse.StatusCode = StatusCode.OK;
            baseResponse.Data = response;
            baseResponse.Description = "GetAssortmentList (int id) successfull\n";
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<Assortment>>()
            {
                Description = "GetAssortmentList (int id) failed",
                StatusCode = StatusCode.NotFound
            };
        }
    } 
    
}