using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Supermarket.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Supermarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // API Đăng Nhập và Sinh JWT
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = AuthenticateUser(request.UserName, request.Password);

            if (user == null)
                return Unauthorized();

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        // API Xác Thực Token
        [HttpGet("auth")]
        public IActionResult AuthenticatedEndpoint()
        {
            // Kiểm tra tên người dùng từ token
            var userName = User.Identity.Name;  // Lấy tên người dùng từ token đã được giải mã
            return Ok($"Hello, {userName}!");
        }

        // Kiểm Tra Người Dùng
        private User AuthenticateUser(string userName, string password)
        {
            // Kiểm tra người dùng và mật khẩu trong cơ sở dữ liệu
            if (userName == "admin" && password == "123")
            {
                return new User { IdUser = 1, UserName = "admin" };
            }

            return null;
        }

        // Sinh JWT
        private string GenerateJwtToken(User user)
        {
            var claims = new[] {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString())
    };

            // Sử dụng khóa bí mật dài 256 bit (32 ký tự)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_a_very_strong_secret_key_256bits"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
