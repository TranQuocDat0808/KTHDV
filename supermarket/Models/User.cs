using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class User
    {
        [Key]  // Đánh dấu thuộc tính này là khóa chính
        public int IdUser { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }
    }
}
