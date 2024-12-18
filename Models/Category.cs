﻿using System.Text.Json.Serialization;

namespace thoeun_coffee.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public List<Product> Products { get; set; } = new List<Product>();  // Capitalized property name
    }
}
