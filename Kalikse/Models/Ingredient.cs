namespace Kalikse.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public Store AvailableStore { get; set; }
        public string PriceRange => $"₱{MinPrice:N2} - ₱{MaxPrice:N2}";
    }
} 