using JWTHLAPI.DataLayer.Interfaces.Hotel;
using JWTHLAPI.ModelLayer.DTO.Hotel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JWTHLAPI.BusinessLayer.Manager.Hotel
{
    public class CategoryManager
    {
        private readonly ICategoryRepository _repo;

        public CategoryManager(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public Task<int> Create(CategoryCreateUpdateRequest request) => _repo.Create(request);
        public Task<int> Update(CategoryCreateUpdateRequest request) => _repo.Update(request);
        public Task<int> Delete(int id) => _repo.Delete(id);
        public Task<IEnumerable<CategoryResponse>> GetAll() => _repo.GetAll();
        public Task<CategoryResponse> GetById(int id) => _repo.GetById(id);
    }
}