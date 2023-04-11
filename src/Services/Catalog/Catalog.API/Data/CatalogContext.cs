using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        #region Declare
        public IMongoCollection<Product> Products { get; }
        #endregion

        #region Constructor
        public CatalogContext(IConfiguration configuration) { 
            // lấy thông tin config database
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            // lấy ra thông tin danh mục (bảng) Product
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            // tạo dữ liệu mẫu
            CatalogContextSeed.SeedData(Products);
        }
        #endregion
    }
}
