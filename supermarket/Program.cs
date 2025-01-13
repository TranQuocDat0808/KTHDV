using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,  // Chỉnh sửa nếu cần
        ValidateAudience = false,  // Chỉnh sửa nếu cần
        ValidateLifetime = true,  // Kiểm tra thời gian sống của token
        ValidateIssuerSigningKey = true,  // Kiểm tra khóa ký token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_a_very_strong_secret_key_256bits"))  // Khóa bí mật 256 bit
    };
});

builder.Services.AddControllers();

var app = builder.Build();

// Kích hoạt Swagger và Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Supermarket API v1");
    c.RoutePrefix = "swagger";  // Đặt Swagger UI để truy cập tại /swagger
});

// Bật Authentication và Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
