using Catalog.API.Entities;
using System;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// Lấy danh sách tất cả sản phẩm
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetProducts();
        /// <summary>
        /// Lấy thông tin sản phẩm theo id
        /// </summary>
        /// <param name="id">khóa chính</param>
        /// <returns></returns>
        Task<Product> GetProduct(string id);
        /// <summary>
        /// Lấy thông tin sản phẩm theo tên
        /// </summary>
        /// <param name="name">tên sản phẩm</param>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetProductByName(string name);
        /// <summary>
        /// Lấy thông tin sản phẩm theo tên loại
        /// </summary>
        /// <param name="name">tên loại</param>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);
        /// <summary>
        /// Tạo mới 1 sản phẩm
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        Task CreateProduct(Product product);
        /// <summary>
        /// Sửa 1 sản phẩm
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        Task<bool> UpdateProduct(Product product);
        /// <summary>
        /// Xóa 1 sản phẩm
        /// </summary>
        /// <param name="id">khóa chính</param>
        /// <returns></returns>
        Task<bool> DeleteProduct(string id);
    }
}
