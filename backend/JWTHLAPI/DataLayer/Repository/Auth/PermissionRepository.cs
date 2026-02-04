using System.Threading.Tasks;
using Dapper;
using JWTHLAPI.DataLayer.Interfaces.Auth;
using JWTHLAPI.ModelLayer.Common;

namespace JWTHLAPI.DataLayer.Repository.Auth
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly HMDBContext _dbContext;

        public PermissionRepository(HMDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> HasPermission(
            int roleId,
            string formName,
            PermissionType permission)
        {
            string column = permission switch
            {
                PermissionType.Read => "CanRead",
                PermissionType.GetById => "CanRead",
                PermissionType.GetAll => "CanRead",
                PermissionType.Create => "CanCreate",
                PermissionType.Update => "CanUpdate",
                PermissionType.Delete => "CanDelete",
                
                _ => null
            };

            if (column == null)
                return false;

            var sql = $@"
                SELECT COUNT(1)
                FROM RoleFormPermission RP
                INNER JOIN Forms F ON RP.FormId = F.Id
                WHERE RP.RoleId = @RoleId
                  AND F.FormName = @FormName
                  AND RP.{column} = 1
                  AND RP.IsActive = 1
                  AND F.IsActive = 1";

            using var connection = _dbContext.CreateConnection();

            return await connection.ExecuteScalarAsync<int>(
                sql,
                new { RoleId = roleId, FormName = formName }
            ) > 0;
        }
    }
}