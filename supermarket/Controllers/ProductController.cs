using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supermarket.Models;
using Supermarket.Services;

namespace Supermarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // Lấy danh sách tất cả sản phẩm
        [HttpGet]
        [Authorize] // Xác thực qua JWT
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        // Lấy thông tin chi tiết một sản phẩm
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound(); // Nếu không tìm thấy sản phẩm
            }
            return Ok(product);
        }

        // Thêm một sản phẩm mới
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(); // Kiểm tra nếu sản phẩm là null
            }

            await _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product); // Trả về thông tin sản phẩm đã được tạo
        }

        // Cập nhật thông tin sản phẩm
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Product ID mismatch.");
            }

            try
            {
                await _productService.UpdateProduct(product);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", details = ex.Message });
            }
        }


        // Xóa một sản phẩm
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound(); // Nếu không tìm thấy sản phẩm
            }

            await _productService.DeleteProduct(id);
            return NoContent(); // Trả về mã trạng thái 204 nếu xóa thành công
        }
    }
}
