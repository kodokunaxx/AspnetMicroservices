using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        #region Declare
        private readonly IDiscountRepository _repository;
        #endregion

        #region Constructor
        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy thông tin giảm giá
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var coupon = await _repository.GetDiscount(productName);
            return Ok(coupon);
        }

        /// <summary>
        /// Sửa giảm giá
        /// </summary>
        /// <param name="coupon">coupon</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            var res = await _repository.CreateDiscount(coupon);
            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }

        /// <summary>
        /// Sửa giảm giá
        /// </summary>
        /// <param name="coupon">coupon</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon Discount)
        {
            var res = await _repository.UpdateDiscount(Discount);
            return Ok(res);
        }

        /// <summary>
        /// Xóa giảm giá
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        [HttpDelete("{productName}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteDiscount(string productName)
        {
            await _repository.DeleteDiscount(productName);
            return Ok();
        }
        #endregion
    }
}
