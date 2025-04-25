// File: Models/Recipe.cs
using System.Collections.Generic;

namespace Kalikse.Models // Ensure this namespace matches your project structure
{
    public class Recipe
    {
        public string Name { get; set; }
        // This property will hold the filename string provided by the AI
        public string ImageFilename { get; set; } // e.g., "chicken_adobo.png"

        public List<string> Ingredients { get; set; } // List of ingredient names used in the generated recipe
        public List<InstructionStep> Steps { get; set; } // List of instructions with numbers and text

        // You might also ask the AI to provide these based on the recipe it generates
        public List<string> DietaryTags { get; set; } // e.g., ["Low Carb", "Quick"]
        // You could potentially ask the AI to confirm if it meets the user's budget/diet/allergens
        // and include those confirmations here if needed for display.
    }
}