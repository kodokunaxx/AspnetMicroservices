using Discount.API.Entities;

namespace Discount.API.Repositories
{
    public interface IDiscountRepository
    {
        /// <summary>
        /// Lấy thông tin giảm giá
        /// </summary>
        /// <param name="productName">Tên sp</param>
        /// <returns></returns>
        Task<Coupon> GetDiscount(string productName);
        /// <summary>
        /// Tạo giảm giá
        /// </summary>
        /// <param name="coupon">phiếu mua hàng</param>
        /// <returns></returns>
        Task<bool> CreateDiscount(Coupon coupon);
        /// <summary>
        /// Cập nhật giảm giá
        /// </summary>
        /// <param name="coupon">phiếu mua hàng</param>
        /// <returns></returns>
        Task<bool> UpdateDiscount(Coupon coupon);
        /// <summary>
        /// Xóa giảm giá
        /// </summary>
        /// <param name="productName">Tên sp</param>
        /// <returns></returns>
        Task<bool> DeleteDiscount(string productName);

    }
}
