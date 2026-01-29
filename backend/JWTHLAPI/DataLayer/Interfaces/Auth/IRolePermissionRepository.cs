using System.Collections.Generic;
using System.Threading.Tasks;
using JWTHLAPI.ModelLayer.DTO.Auth;

namespace JWTHLAPI.DataLayer.Interfaces.Auth
{
    public interface IRolePermissionRepository
    {
        Task<int> Assign(RolePermissionCreateUpdateRequest request);
        Task<int> Update(int id, RolePermissionCreateUpdateRequest request);
        Task<int> Delete(int id);
        Task<IEnumerable<RolePermissionResponse>> GetByRole(int roleId);
    }
}