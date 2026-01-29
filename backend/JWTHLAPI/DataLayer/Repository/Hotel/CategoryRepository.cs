using Dapper;
using JWTHLAPI.DataLayer.Interfaces.Hotel;
using JWTHLAPI.ModelLayer.DTO.Hotel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTHLAPI.DataLayer.Repository.Hotel
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly HMDBContext _dbContext;

        public CategoryRepository(HMDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(CategoryCreateUpdateRequest request)
        {
            var sql = @"INSERT INTO HotelCategory
                        (CounterId, CategoryName, DisplayOrder)
                        VALUES (@CounterId, @CategoryName, @DisplayOrder)";
            using var con = _dbContext.CreateConnection();
            return await con.ExecuteAsync(sql, request);
        }

        public async Task<int> Update(CategoryCreateUpdateRequest request)
        {
            var sql = @"UPDATE HotelCategory
                        SET CounterId = @CounterId,
                            CategoryName = @CategoryName,
                            DisplayOrder = @DisplayOrder,
                            UpdatedAt = GETDATE()
                        WHERE Id = @Id";
            using var con = _dbContext.CreateConnection();
            return await con.ExecuteAsync(sql, request);
        }

        public async Task<int> Delete(int id)
        {
            var sql = @"UPDATE HotelCategory
                        SET IsActive = 0,
                            UpdatedAt = GETDATE()
                        WHERE Id = @Id";
            using var con = _dbContext.CreateConnection();
            return await con.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<CategoryResponse>> GetAll()
        {
            var sql = @"SELECT C.Id, C.CounterId, HC.CounterName,
                               C.CategoryName, C.DisplayOrder
                        FROM HotelCategory C
                        INNER JOIN HotelCounter HC ON C.CounterId = HC.Id
                        WHERE C.IsActive = 1 AND HC.IsActive = 1";
            using var con = _dbContext.CreateConnection();
            return await con.QueryAsync<CategoryResponse>(sql);
        }

        public async Task<CategoryResponse> GetById(int id)
        {
            var sql = @"SELECT C.Id, C.CounterId, HC.CounterName,
                               C.CategoryName, C.DisplayOrder
                        FROM HotelCategory C
                        INNER JOIN HotelCounter HC ON C.CounterId = HC.Id
                        WHERE C.Id = @Id AND C.IsActive = 1";
            using var con = _dbContext.CreateConnection();
            return (await con.QueryAsync<CategoryResponse>(sql, new { Id = id }))
                   .FirstOrDefault();
        }
    }
}