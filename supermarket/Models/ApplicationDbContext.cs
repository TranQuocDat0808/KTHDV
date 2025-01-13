using Microsoft.EntityFrameworkCore;

namespace Supermarket.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Định nghĩa các DbSet cho các bảng trong cơ sở dữ liệu
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
