namespace Kalikse.Models;

public class Recipe
{
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public decimal PriceRangeMin { get; set; }
    public decimal PriceRangeMax { get; set; }
    public List<RecipeIngredient> Ingredients { get; set; }
    public List<string> DietaryPreferences { get; set; }
}

public class RecipeIngredient
{
    public string Name { get; set; }
    public string Quantity { get; set; }
    public string Unit { get; set; }
}