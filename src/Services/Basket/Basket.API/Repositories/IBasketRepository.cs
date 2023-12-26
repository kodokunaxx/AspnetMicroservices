using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        /// <summary>
        /// Lấy thông tin giỏ hàng theo tên người dùng
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<ShoppingCart> GetBasket(string username);
        /// <summary>
        /// Thay đổi thông tin giỏ hàng
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        /// <summary>
        /// Xóa giỏ hàng
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task DeleteBasket(string username);
    }
}
