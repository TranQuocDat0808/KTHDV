using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Supermarket.Middlewares;
using Supermarket.Services;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký các dịch vụ
builder.Services.AddSingleton<TokenService>();  // Đăng ký TokenService
builder.Services.AddControllers();  // Đăng ký controllers

// Cấu hình Swagger (nếu cần thiết)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cấu hình middleware
app.UseMiddleware<JwtMiddleware>();  // Middleware xác thực JWT

// Cấu hình Swagger (nếu cần thiết)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Tạo swagger endpoint
    app.UseSwaggerUI();  // Hiển thị Swagger UI
}

// Map các API controllers
app.MapControllers();  // Sử dụng các controllers cho API

app.Run();  // Chạy ứng dụng
