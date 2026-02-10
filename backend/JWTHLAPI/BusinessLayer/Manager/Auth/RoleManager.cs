using JWTHLAPI.DataLayer.Interfaces.Auth;
using JWTHLAPI.ModelLayer.DTO.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JWTHLAPI.BusinessLayer.Manager.Auth
{
    public class RoleManager
    {
        private readonly IRoleRepository _repo;

        public RoleManager(IRoleRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RoleResponseDto>> GetAll()
        {
            return await (_repo.GetAll());
        }
    }
}