using Dapper;
using JWTHLAPI.DataLayer.Interfaces.Hotel;
using JWTHLAPI.ModelLayer.DTO.Hotel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTHLAPI.DataLayer.Repository.Hotel
{
    public class CounterRepository : ICounterRepository
    {
        private readonly HMDBContext _dbContext;

        public CounterRepository(HMDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(CounterCreateUpdateRequest request)
        {
            var sql = @"INSERT INTO HotelCounter (CounterName)
                        VALUES (@CounterName)";
            using var con = _dbContext.CreateConnection();
            return await con.ExecuteAsync(sql, request);
        }

        public async Task<int> Update(CounterCreateUpdateRequest request)
        {
            var sql = @"UPDATE HotelCounter
                        SET CounterName = @CounterName,
                            UpdatedAt = GETDATE()
                        WHERE Id = @Id";
            using var con = _dbContext.CreateConnection();
            return await con.ExecuteAsync(sql, request);
        }

        public async Task<int> Delete(int id)
        {
            var sql = @"UPDATE HotelCounter
                        SET IsActive = 0,
                            UpdatedAt = GETDATE()
                        WHERE Id = @Id";
            using var con = _dbContext.CreateConnection();
            return await con.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<CounterResponse>> GetAll()
        {
            var sql = @"SELECT Id, CounterName
                        FROM HotelCounter
                        WHERE IsActive = 1";
            using var con = _dbContext.CreateConnection();
            return await con.QueryAsync<CounterResponse>(sql);
        }

        public async Task<CounterResponse> GetById(int id)
        {
            var sql = @"SELECT Id, CounterName
                        FROM HotelCounter
                        WHERE Id = @Id AND IsActive = 1";
            using var con = _dbContext.CreateConnection();
            return (await con.QueryAsync<CounterResponse>(sql, new { Id = id }))
                   .FirstOrDefault();
        }
    }
}