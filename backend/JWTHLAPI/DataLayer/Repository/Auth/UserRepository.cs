using Dapper;
using JWTHLAPI.ModelLayer.Entities.Auth;
using JWTHLAPI.DataLayer.Interfaces.Auth;
using System.Linq;
using System.Threading.Tasks;

namespace JWTHLAPI.DataLayer.Repository.Auth
{
    public class UserRepository : IUserRepository
    {
        private readonly HMDBContext _dbContext;

        public UserRepository(HMDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByUsername(string userName)
        {
            var sql = @"
                SELECT  
                    U.Id,
                    U.UserName,
                    U.PasswordHash,
                    U.PasswordSalt,
                    U.RoleId,
                    R.RoleName,
                    U.IsActive
                FROM dbo.Users U
                INNER JOIN dbo.Roles R ON U.RoleId = R.Id
                WHERE U.Username = @UserName
                  AND U.IsActive = 1
                  AND R.IsActive = 1";

            using (var connection = _dbContext.CreateConnection())
            {
                return (await connection.QueryAsync<User>(
                    sql,
                    new { UserName = userName }
                )).FirstOrDefault();
            }
        }

        public async Task<bool> IsUsernameExists(string userName)
        {
            var sql = @"SELECT COUNT(1) FROM dbo.Users WHERE Username = @UserName";

            using (var connection = _dbContext.CreateConnection())
            {
                var count = await connection.ExecuteScalarAsync<int>(
                    sql,
                    new { UserName = userName }
                );

                return count > 0;
            }
        }

        public async Task<int> Create(User user)
        {
            var sql = @"
                INSERT INTO dbo.Users
                (
                    Username,
                    PasswordHash,
                    PasswordSalt,
                    RoleId,
                    CreatedBy
                )
                VALUES
                (
                    @Username,
                    @PasswordHash,
                    @PasswordSalt,
                    @RoleId,
                    @CreatedBy
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            using (var connection = _dbContext.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, user);
            }
        }
    }
}