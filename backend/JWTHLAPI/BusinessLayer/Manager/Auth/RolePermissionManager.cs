using System.Collections.Generic;
using System.Threading.Tasks;
using JWTHLAPI.DataLayer.Interfaces.Auth;
using JWTHLAPI.ModelLayer.DTO.Auth;

namespace JWTHLAPI.BusinessLayer.Manager.Auth
{
    public class RolePermissionManager
    {
        private readonly IRolePermissionRepository _repo;

        public RolePermissionManager(IRolePermissionRepository repo)
        {
            _repo = repo;
        }

        public Task<int> Assign(RolePermissionCreateUpdateRequest req)
            => _repo.Assign(req);

        public Task<int> Update(int id, RolePermissionCreateUpdateRequest req)
            => _repo.Update(id, req);

        public Task<int> Delete(int id)
            => _repo.Delete(id);

        public Task<IEnumerable<RolePermissionResponse>> GetByRole(int roleId)
            => _repo.GetByRole(roleId);
        public Task<IEnumerable<RolePermissionResponse>> GetAll()
            => _repo.GetAll();
    }
}