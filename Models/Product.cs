using System.Text.Json.Serialization;

namespace thoeun_coffee.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }  // Removed duplicate declaration
        public int? InventoryId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public Category? Category { get; set; }  // This is the navigation property

        [JsonIgnore]
        public Inventory? Inventories { get; set; }
        [JsonIgnore]
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        [JsonIgnore]
        public List<Review> Reviews { get; set; } = new List<Review>();
        [JsonIgnore]
        public List<Discount> Discounts { get; set; } = new List<Discount>();
    }
}
