using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        #region Declare
        private readonly IBasketRepository _repository;
        #endregion

        #region Constructor
        public BasketController(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy thông tin giỏ hàng
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("{username}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string username)
        {
            var basket = await _repository.GetBasket(username);
            return Ok(basket ?? new ShoppingCart(username));
        }

        /// <summary>
        /// Sửa giỏ hàng
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            var res = await _repository.UpdateBasket(basket);
            return Ok(res);
        }

        /// <summary>
        /// Xóa giỏ hàng
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        [HttpDelete("{username}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            await _repository.DeleteBasket(username);
            return Ok();
        }
        #endregion


    }
}
