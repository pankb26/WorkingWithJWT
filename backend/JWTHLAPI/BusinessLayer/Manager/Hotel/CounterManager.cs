using JWTHLAPI.DataLayer.Interfaces.Hotel;
using JWTHLAPI.ModelLayer.DTO.Hotel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JWTHLAPI.BusinessLayer.Manager.Hotel
{
    public class CounterManager
    {
        private readonly ICounterRepository _repo;

        public CounterManager(ICounterRepository repo)
        {
            _repo = repo;
        }

        public Task<int> Create(CounterCreateUpdateRequest request) => _repo.Create(request);
        public Task<int> Update(CounterCreateUpdateRequest request) => _repo.Update(request);
        public Task<int> Delete(int id) => _repo.Delete(id);
        public Task<IEnumerable<CounterResponse>> GetAll() => _repo.GetAll();
        public Task<CounterResponse> GetById(int id) => _repo.GetById(id);
    }
}