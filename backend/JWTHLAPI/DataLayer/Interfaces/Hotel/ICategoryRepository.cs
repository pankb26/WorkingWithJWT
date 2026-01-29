using System.Collections.Generic;
using System.Threading.Tasks;
using JWTHLAPI.ModelLayer.DTO.Hotel;

namespace JWTHLAPI.DataLayer.Interfaces.Hotel
{
    public interface ICategoryRepository
    {
        Task<int> Create(CategoryCreateUpdateRequest request);
        Task<int> Update(CategoryCreateUpdateRequest request);
        Task<int> Delete(int id);
        Task<IEnumerable<CategoryResponse>> GetAll();
        Task<CategoryResponse> GetById(int id);
    }
}