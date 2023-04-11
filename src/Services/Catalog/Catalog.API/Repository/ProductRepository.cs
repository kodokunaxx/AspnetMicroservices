using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;

namespace Catalog.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Lấy danh sách tất cả sản phẩm
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        /// <summary>
        /// Lấy thông tin sản phẩm theo id
        /// </summary>
        /// <param name="id">khóa chính</param>
        /// <returns></returns>
        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products.Find(p => p.ProductId == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Lấy thông tin sản phẩm theo tên
        /// </summary>
        /// <param name="name">tên sản phẩm</param>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            // khởi tạo bộ lọc để có thể dễ dàng build biểu thức lọc điều kiện ( có thể dùng trực tiếp giống LINQ Find(x => x.Prop == param) nhưng khó ghép nhiều điều kiện)
            var filter = Builders<Product>.Filter.Eq(p => p.ProductName, name);
            return await _context.Products.Find(filter).ToListAsync();
        }

        /// <summary>
        /// Lấy thông tin sản phẩm theo tên loại
        /// </summary>
        /// <param name="name">tên loại</param>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            return await _context.Products.Find(filter).ToListAsync();
        }

        /// <summary>
        /// Tạo mới 1 sản phẩm
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        /// <summary>
        /// Sửa 1 sản phẩm
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        public async Task<bool> UpdateProduct(Product product)
        {
            // thực hiện update
            var updateResult = await _context.Products.ReplaceOneAsync(filter: g => g.ProductId == product.ProductId, replacement: product);
            //return
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Xóa 1 sản phẩm
        /// </summary>
        /// <param name="id">khóa chính</param>
        /// <returns></returns>
        public async Task<bool> DeleteProduct(string id)
        {
            // thực hiện xóa
            var deleteResult = await _context.Products.DeleteOneAsync(filter: g => g.ProductId == id);
            //return
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
