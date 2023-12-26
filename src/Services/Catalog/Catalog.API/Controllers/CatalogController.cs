using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Xml.Linq;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        #region Declare
        private readonly IProductRepository _repository;
        private ILogger<CatalogController> _logger;
        #endregion

        #region Constructor
        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy danh sách tất cả sản phẩm
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repository.GetProducts();
            if (!products.Any())
            {
                _logger.LogError($"Collection Products is empty.");
                return NotFound();
            }
            return Ok(products);
        }

        /// <summary>
        /// Lấy thông tin sản phẩm theo id
        /// </summary>
        /// <param name="id">khóa chính</param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _repository.GetProduct(id);
            if (product == null)
            {
                _logger.LogError($"Product with id: {id} is not found.");
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Lấy thông tin sản phẩm theo tên
        /// </summary>
        /// <param name="id">khóa chính</param>
        /// <returns></returns>
        [Route("[action]/{name}", Name = "GetProductByName")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        {
            var products = await _repository.GetProductByName(name);
            if (!products.Any())
            {
                _logger.LogError($"Products with name: {name} are not found.");
                return NotFound();
            }
            return Ok(products);
        }

        /// <summary>
        /// Lấy thông tin sản phẩm theo loại
        /// </summary>
        /// <param name="id">khóa chính</param>
        /// <returns></returns>
        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var products = await _repository.GetProductByCategory(category);
            if (!products.Any())
            {
                _logger.LogError($"Products with category: {category} are not found.");
                return NotFound();
            }
            return Ok(products);
        }

        /// <summary>
        /// Tạo mới 1 sản phẩm
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _repository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new {ProductId = product.ProductId}, product);
        }

        /// <summary>
        /// Sửa 1 sản phẩm
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            var res = await _repository.UpdateProduct(product);
            return res ? Ok(res) : UnprocessableEntity(res);
        }

        /// <summary>
        /// Xóa 1 sản phẩm
        /// </summary>
        /// <param name="id">khóa chính</param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var res = await _repository.DeleteProduct(id);
            return res ? Ok(res) : UnprocessableEntity(res);
        }
        #endregion
    }
}
