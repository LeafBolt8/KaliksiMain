using System.Collections.Generic;

namespace Kalikse.Models
{
    public class Store
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public List<string> Branches { get; set; }
    }

    public class Ingredient
    {
        public string Name { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public Store AvailableStore { get; set; }
    }
} 