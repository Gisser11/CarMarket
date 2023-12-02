using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.StudiaViewModel;

namespace Market.Service.Interfaces;

public interface IStudiaService
{
    Task<IBaseResponse<IEnumerable<Studia>>> GetAllStudia();

    Task<IBaseResponse<StudiaViewModel>> CreateStudia(StudiaViewModel studiaViewModel);
    
    
}