using System.Threading.Tasks;
using JWTHLAPI.DataLayer.Interfaces.Auth;
using JWTHLAPI.ModelLayer.Common;

namespace JWTHLAPI.BusinessLayer.Manager.Auth
{
    public class PermissionManager
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionManager(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<bool> HasPermission(
            int roleId,
            string formName,
            PermissionType permission)
        {
            return await _permissionRepository.HasPermission(
                roleId, formName, permission);
        }
    }
}