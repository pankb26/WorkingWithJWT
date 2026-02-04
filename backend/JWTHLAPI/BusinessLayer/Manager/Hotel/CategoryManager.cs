//using JWTHLAPI.DataLayer.Interfaces.Hotel;
//using JWTHLAPI.ModelLayer.DTO.Hotel;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace JWTHLAPI.BusinessLayer.Manager.Hotel
//{
//    public class CategoryManager
//    {
//        private readonly ICategoryRepository _repo;

//        public CategoryManager(ICategoryRepository repo)
//        {
//            _repo = repo;
//        }

//        public Task<int> Create(CategoryCreateUpdateRequest request) => _repo.Create(request);
//        public Task<int> Update(CategoryCreateUpdateRequest request) => _repo.Update(request);
//        public Task<int> Delete(int id) => _repo.Delete(id);
//        public Task<IEnumerable<CategoryResponse>> GetAll() => _repo.GetAll();
//        public Task<CategoryResponse> GetById(int id) => _repo.GetById(id);
//    }
//}
using System.Threading.Tasks;
using JWTHLAPI.DataLayer.Interfaces.Hotel;
using JWTHLAPI.ModelLayer.Common;
using JWTHLAPI.ModelLayer.DTO.Hotel;

namespace JWTHLAPI.BusinessLayer.Manager.Hotel
{
    public class CategoryManager
    {
        private readonly ICategoryRepository _repo;

        public CategoryManager(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApiResponse> GetAll()
        {
            var data = await _repo.GetAll();
            return new ApiResponse(true, "Success", data);
        }

        public async Task<ApiResponse> GetById(int id)
        {
            var data = await _repo.GetById(id);
            if (data == null)
                return new ApiResponse(false, "Record not found");

            return new ApiResponse(true, "Success", data);
        }

        public async Task<ApiResponse> Create(CategoryCreateUpdateRequest request)
        {
            var result = await _repo.Create(request);
            if (result <= 0)
                return new ApiResponse(false, "Record not inserted");

            return new ApiResponse(true, "Inserted successfully",result);
        }

        public async Task<ApiResponse> Update(CategoryCreateUpdateRequest request)
        {
            var result = await _repo.Update(request);
            if (result <= 0)
                return new ApiResponse(false, "Record not updated");

            return new ApiResponse(true, "Updated successfully");
        }

        public async Task<ApiResponse> Delete(int id)
        {
            var result = await _repo.Delete(id);
            if (result <= 0)
                return new ApiResponse(false, "Record not found");

            return new ApiResponse(true, "Deleted successfully");
        }
    }
}