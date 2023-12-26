using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;
        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        /// <summary>
        /// Thay đổi thông tin giỏ hàng
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        public async Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await _redisCache.GetStringAsync(username);
            if (!string.IsNullOrEmpty(basket))
            {
                return JsonConvert.DeserializeObject<ShoppingCart>(basket);
            }
            return null;
        }

        /// <summary>
        /// Xóa giỏ hàng
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserName);
        }

        /// <summary>
        /// Lấy thông tin giỏ hàng theo tên người dùng
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task DeleteBasket(string username)
        {
            await _redisCache.RemoveAsync(username);
        }
    }
}
