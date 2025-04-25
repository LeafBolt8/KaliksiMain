// File: Data/LocalData.cs
using System.Collections.Generic;
using Kalikse.Models; // Import your Models namespace

namespace Kalikse.Data // Ensure this namespace matches your project structure
{
    public static class LocalRecipeData
    {
        // This list defines the ingredients your AI is allowed to use for generation.
        public static List<Ingredient> AvailableIngredients = new List<Ingredient>
        {
            // Populate this list with the ingredients you want the AI to use.
            // Include properties like Name, Cost, DietaryInfo, Allergens.
            // Example Filipino Ingredients (feel free to replace/expand):
            new Ingredient { Name = "Garlic", Cost = 5.0, DietaryInfo = new List<string>{"Vegan", "Vegetarian", "Gluten-Free"}, Allergens = new List<string>() },
            new Ingredient { Name = "Onion", Cost = 7.0, DietaryInfo = new List<string>{"Vegan", "Vegetarian", "Gluten-Free"}, Allergens = new List<string>() },
            new Ingredient { Name = "Ginger", Cost = 8.0, DietaryInfo = new List<string>{"Vegan", "Vegetarian", "Gluten-Free"}, Allergens = new List<string>() },
            new Ingredient { Name = "Soy Sauce", Cost = 15.0, DietaryInfo = new List<string>{"Vegan", "Vegetarian"}, Allergens = new List<string>{"Soy", "Wheat"} }, // Wheat means Gluten
            new Ingredient { Name = "Vinegar", Cost = 10.0, DietaryInfo = new List<string>{"Vegan", "Vegetarian", "Gluten-Free"}, Allergens = new List<string>() },
            new Ingredient { Name = "Cooking Oil", Cost = 20.0, DietaryInfo = new List<string>{"Vegan", "Vegetarian", "Gluten-Free"}, Allergens = new List<string>() },
            new Ingredient { Name = "Rice", Cost = 15.0, DietaryInfo = new List<string>{"Gluten-Free"}, Allergens = new List<string>() },
            new Ingredient { Name = "Chicken", Cost = 150.0, DietaryInfo = new List<string>{"High Protein"}, Allergens = new List<string>() },
            new Ingredient { Name = "Pork", Cost = 180.0, DietaryInfo = new List<string>{"High Protein"}, Allergens = new List<string>() },
            new Ingredient { Name = "Beef", Cost = 250.0, DietaryInfo = new List<string>{"High Protein"}, Allergens = new List<string>() },
            new Ingredient { Name = "Tilapia", Cost = 100.0, DietaryInfo = new List<string>{"High Protein"}, Allergens = new List<string>{"Fish"} },
            new Ingredient { Name = "Coconut Milk", Cost = 40.0, DietaryInfo = new List<string>{"Vegan", "Vegetarian", "Gluten-Free"}, Allergens = new List<string>() },
            // Add more ingredients here... aim for at least 20 if you want variety as discussed before.
            // The more detailed the ingredient properties (Cost, DietaryInfo, Allergens), the better you can filter
            // which ingredients to include in the prompt based on user input, or ask the AI to consider them.
        };

        // You could add other local data here if needed (e.g., predefined image filename mappings)
    }
}