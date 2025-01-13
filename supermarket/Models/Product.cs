public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    // Sửa tên thuộc tính để khớp với cột trong DB
    public DateTime CreatedAt { get; set; } // changed from 'createdAt' to 'CreatedAt'
    public DateTime UpdatedAt { get; set; } // changed from 'updatedAt' to 'UpdatedAt'
}
