namespace Discount.Grpc.Entities
{
    public class Coupon
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Tên sp
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Giá tiền
        /// </summary>
        public int Amount { get; set; }
    }
}
