// File: Models/Ingredient.cs
using System.Collections.Generic;

namespace Kalikse.Models // Ensure this namespace matches your project structure
{
    public class Ingredient
    {
        public string Name { get; set; }
        // You might add properties like Cost, DietaryInfo, Allergens here if you need them
        // for filtering which ingredients to include in the prompt based on user input BEFORE sending to AI
        // For the simplest start, just Name is enough for the AI list, but including others is good for future filtering.
        public double Cost { get; set; } // Example: Cost per unit
        public List<string> DietaryInfo { get; set; } // e.g., ["Vegan", "Gluten-Free"]
        public List<string> Allergens { get; set; } // e.g., ["Peanuts", "Dairy"]
    }
}