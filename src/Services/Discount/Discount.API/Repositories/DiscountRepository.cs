using Dapper;
using Discount.API.Entities;
using Npgsql;
using System.Data;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;
        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }
        public async Task<Coupon> GetDiscount(string productName)
        {
            var coupon = await _connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon where ProductName = @ProductName", new { ProductName = productName });
            if(coupon == null)
            {
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
            }
            return coupon;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var res = await _connection.ExecuteAsync("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)", coupon);
            return res == 0 ? false : true;        
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var res = await _connection.ExecuteAsync("UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id", coupon);
            return res == 0 ? false : true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var res = await _connection.ExecuteAsync("DELETE Coupon WHERE ProductName = @ProductName", new {ProductName = productName});
            return res == 0 ? false : true;
        }

        public void Dispose()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
