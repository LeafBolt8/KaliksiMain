using System;
using System.Collections.Generic;

namespace Kalikse.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string DietaryPreference { get; set; } // e.g., "Balanced", "Vegetarian", "Vegan"
        public List<string> Allergens { get; set; }
        public List<string> Ingredients { get; set; }
        public string Instructions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 