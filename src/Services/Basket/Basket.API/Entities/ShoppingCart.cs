namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        /// <summary>
        /// Tên người dùng
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Danh sách sản phẩm mua
        /// </summary>
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        /// <summary>
        /// Tổng tiền, tự động tính
        /// </summary>
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }
        public ShoppingCart()
        {
            
        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
    }
}
