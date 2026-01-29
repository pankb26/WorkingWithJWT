using System.Collections.Generic;
using System.Threading.Tasks;
using JWTHLAPI.ModelLayer.DTO.Hotel;

namespace JWTHLAPI.DataLayer.Interfaces.Hotel
{
    public interface ICounterRepository
    {
        Task<int> Create(CounterCreateUpdateRequest request);
        Task<int> Update(CounterCreateUpdateRequest request);
        Task<int> Delete(int id);
        Task<IEnumerable<CounterResponse>> GetAll();
        Task<CounterResponse> GetById(int id);
    }
}