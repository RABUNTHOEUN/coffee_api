using System.ComponentModel.DataAnnotations;

namespace thoeun_coffee.Models
{
    public class CoffeeBean
    {
        [Key]
        public int BeanId { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public string RoastLevel { get; set; } // 'light', 'medium', 'dark'
        public decimal PricePerKg { get; set; }
        public int StockQuantity { get; set; }
    }
}
