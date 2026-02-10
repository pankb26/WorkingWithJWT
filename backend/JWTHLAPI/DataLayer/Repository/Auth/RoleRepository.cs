using Dapper;
using JWTHLAPI.DataLayer.Interfaces.Auth;
using JWTHLAPI.ModelLayer.DTO.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JWTHLAPI.DataLayer.Repository.Auth
{
    public class RoleRepository : IRoleRepository
    {
        private readonly HMDBContext _db;

        public RoleRepository(HMDBContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<RoleResponseDto>> GetAll()
        {
            var sql = @"
                SELECT Id, RoleName
                FROM Roles
                WHERE IsActive = 1
                ORDER BY RoleName";

            using var conn = _db.CreateConnection();
            return await conn.QueryAsync<RoleResponseDto>(sql);
        }
    }
}