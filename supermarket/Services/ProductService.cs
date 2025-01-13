using Supermarket.Models;
using Microsoft.EntityFrameworkCore;

namespace Supermarket.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả sản phẩm
        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // Lấy sản phẩm theo id
        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProduct(Product product)
        {
            product.CreatedAt = DateTime.UtcNow; // Gán thời gian hiện tại cho CreatedAt
            product.UpdatedAt = DateTime.UtcNow; // Gán thời gian hiện tại cho UpdatedAt
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }


        // Cập nhật sản phẩm
        public async Task UpdateProduct(Product product)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

            if (existingProduct == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            // Cập nhật các thuộc tính của thực thể đã được theo dõi
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;

            await _context.SaveChangesAsync();
        }



        // Xóa sản phẩm
        public async Task DeleteProduct(int id)
        {
            var product = await GetProductById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
