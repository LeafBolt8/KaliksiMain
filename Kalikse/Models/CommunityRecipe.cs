using System; // Needed for DateTime

namespace Kalikse.Models // Ensure this matches your project namespace + .Models
{
    // Represents a recipe shared in the community feed
    public class CommunityRecipe
    {
        public string UserName { get; set; }
        public DateTime PostDate { get; set; }
        public string FoodName { get; set; }
        public string IngredientsWithPrices { get; set; } // Storing as string for simplicity now
        public string Description { get; set; }
        public string ImageUrl { get; set; } // Assuming image source is a URL or filename

        // Properties for like and comment counts - MAKE SURE THESE ARE PRESENT
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }

        // TODO: Add properties for User ID, Recipe ID, etc. for a real app
    }
}
