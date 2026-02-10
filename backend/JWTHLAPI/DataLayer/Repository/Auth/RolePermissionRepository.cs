using Dapper;
using JWTHLAPI.DataLayer.Interfaces.Auth;
using JWTHLAPI.ModelLayer.DTO.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JWTHLAPI.DataLayer.Repository.Auth
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly HMDBContext _db;

        public RolePermissionRepository(HMDBContext db)
        {
            _db = db;
        }

        public async Task<int> Assign(RolePermissionCreateUpdateRequest request)
        {
            var sql = @"IF  EXISTS (SELECT 1 FROM RoleFormPermission WHERE RoleId = @RoleId AND FormId = @FormId AND IsActive = 1) SELECT 0;
                BEGIN
                     INSERT INTO RoleFormPermission
                    (RoleId, FormId, CanRead, CanCreate, CanUpdate, CanDelete,IsActive,CreatedAt)
                    VALUES
                    (@RoleId, @FormId, @CanRead, @CanCreate, @CanUpdate, @CanDelete,1,GETDATE());
                    SELECT 1;
                END
                

               ";

            using var conn = _db.CreateConnection();
            return await conn.ExecuteAsync(sql, request);
        }

        public async Task<int> Update(int id, RolePermissionCreateUpdateRequest request)
        {
            var sql = @"
                UPDATE RoleFormPermission
                SET CanRead=@CanRead,
                    CanCreate=@CanCreate,
                    CanUpdate=@CanUpdate,
                    CanDelete=@CanDelete,
                    UpdatedAt=GETDATE()
                WHERE Id=@Id AND IsActive=0";

            using var conn = _db.CreateConnection();
            return await conn.ExecuteAsync(sql, new { Id = id, request.CanRead, request.CanCreate, request.CanUpdate, request.CanDelete });
        }

        public async Task<int> Delete(int id)
        {
            var sql = "UPDATE RoleFormPermission SET IsActive = 0 WHERE Id=@Id";

            using var conn = _db.CreateConnection();
            return await conn.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<RolePermissionResponse>> GetByRole(int roleId)
        {
            var sql = @"
                SELECT
                    RP.Id,
                    R.RoleName,
                    F.FormName,
                    RP.CanRead,
                    RP.CanCreate,
                    RP.CanUpdate,
                    RP.CanDelete
                FROM RoleFormPermission RP
                INNER JOIN Roles R ON RP.RoleId = R.Id
                INNER JOIN Forms F ON RP.FormId = F.Id
                WHERE RP.RoleId = @RoleId
                  AND RP.IsActive = 1";

            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<RolePermissionResponse>(sql, new { RoleId = roleId });
        }
        public async Task<IEnumerable<RolePermissionResponse>> GetAll()
        {
            var sql = @"
        SELECT
            RP.Id,
            R.RoleName,
            F.FormName,
            RP.CanRead,
            RP.CanCreate,
            RP.CanUpdate,
            RP.CanDelete
        FROM RoleFormPermission RP
        INNER JOIN Roles R ON RP.RoleId = R.Id
        INNER JOIN Forms F ON RP.FormId = F.Id
        WHERE RP.IsActive = 1
        ORDER BY R.RoleName, F.FormName";

            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<RolePermissionResponse>(sql);
        }

    }
}