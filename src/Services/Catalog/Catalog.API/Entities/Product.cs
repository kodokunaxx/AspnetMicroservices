using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities
{
    /// <summary>
    /// Sản phẩm
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        [BsonElement("ProductName")]
        public string ProductName { get; set; }
        /// <summary>
        /// Loại =
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Tóm tắt
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Ảnh file
        /// </summary>
        public string ImageFile { get; set; }
        /// <summary>
        /// Giá
        /// </summary>
        public decimal? Price { get; set; }
    }
}
