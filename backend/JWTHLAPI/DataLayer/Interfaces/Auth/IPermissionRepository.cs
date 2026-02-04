using System.Threading.Tasks;
using JWTHLAPI.ModelLayer.Common;

namespace JWTHLAPI.DataLayer.Interfaces.Auth
{
    public interface IPermissionRepository
    {
        Task<bool> HasPermission(int roleId,string formName,PermissionType permission);
    }
}