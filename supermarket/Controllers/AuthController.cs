using Microsoft.AspNetCore.Mvc;
using Supermarket.Models;
using Supermarket.Services;

namespace Supermarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Kiểm tra người dùng và mật khẩu (trong thực tế, bạn cần mã hóa mật khẩu)
            var user = GetUserFromDatabase(request.UserName, request.Password); // Thay bằng cách kiểm tra từ cơ sở dữ liệu

            if (user == null)
            {
                return Unauthorized("Thông tin đăng nhập không hợp lệ");
            }

            // Sinh JWT token
            var token = _tokenService.GenerateToken(user.UserName);
            return Ok(new { Token = token });
        }

        // Giả sử bạn có một hàm kiểm tra người dùng từ cơ sở dữ liệu
        private User GetUserFromDatabase(string username, string password)
        {
            // Đọc từ cơ sở dữ liệu (bạn có thể thay bằng bất kỳ cách nào bạn sử dụng)
            return new User { UserName = "admin", Password = "password" };  // Ví dụ đơn giản
        }
    }
}
