using System.Text.Json.Serialization;

namespace thoeun_coffee.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int? ProductId { get; set; }
        public int StockQuantity { get; set; }
        public DateTime? RestockDate { get; set; }

        public Product? Product { get; set; }
    }
}
