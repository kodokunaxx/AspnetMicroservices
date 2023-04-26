namespace Basket.API.Entities
{
    public class ShoppingCartItem
    {
        /// <summary>
        /// Số lượng
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Màu sắc
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// Giá
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Id sản phẩm
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string ProductName { get; set; }
    }
}