using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using System.Linq;

using Kalikse.Models; // Add this using directive to find CommunityRecipe

namespace Kalikse
{
    public partial class CommunityFeedPage : ContentPage
    {
        // Use ObservableCollection so the UI updates automatically when items are added
        public ObservableCollection<CommunityRecipe> Recipes { get; set; }

        public CommunityFeedPage()
        {
            InitializeComponent();

            // Initialize the collection
            Recipes = new ObservableCollection<CommunityRecipe>();

            // Add some dummy data for testing the UI initially
            LoadDummyRecipes();

            // Set the BindingContext so the CollectionView can find the Recipes collection
            BindingContext = this;
        }

        // Dummy data loading method (keep this for initial display)
        private void LoadDummyRecipes()
        {
            // Clear existing dummy data if necessary before adding more
            Recipes.Clear();

            Recipes.Add(new CommunityRecipe
            {
                UserName = "Chef Hans",
                PostDate = DateTime.Now.AddDays(-2),
                FoodName = "Adobo sa Gata",
                IngredientsWithPrices = "Chicken (₱150), Coconut Milk (₱50), Soy Sauce (₱20), Vinegar (₱10), Garlic (₱5), Onion (₱5)",
                Description = "A creamy and savory version of the classic Filipino adobo, made richer with coconut milk.",
                ImageUrl = "adobo.png", // Ensure this image exists in Resources/Images
                LikesCount = 15, // Added dummy count
                CommentsCount = 3 // Added dummy count
            });

            Recipes.Add(new CommunityRecipe
            {
                UserName = "Maria's Kitchen",
                PostDate = DateTime.Now.AddDays(-1),
                FoodName = "Sinigang na Baboy",
                IngredientsWithPrices = "Pork (₱200), Tamarind Mix (₱30), Kangkong (₱15), Radish (₱10), Tomatoes (₱10), Onions (₱5)",
                Description = "Sour and hearty Filipino soup, perfect for a rainy day.",
                ImageUrl = "sinigang.png", // Ensure this image exists
                LikesCount = 22, // Added dummy count
                CommentsCount = 5 // Added dummy count
            });

            Recipes.Add(new CommunityRecipe
            {
                UserName = "Juan Dela Cruz",
                PostDate = DateTime.Now,
                FoodName = "Pancit Canton",
                IngredientsWithPrices = "Pancit Canton Noodles (₱40), Mixed Vegetables (₱50), Pork/Chicken (₱80), Soy Sauce (₱15), Oyster Sauce (₱10)",
                Description = "Stir-fried noodles with meat and vegetables, a Filipino fiesta staple.",
                ImageUrl = "pancit.png", // Ensure this image exists
                LikesCount = 8, // Added dummy count
                CommentsCount = 1 // Added dummy count
            });

            // TODO: In a real app, load data from a database (like Firebase, etc.)
        }


        // Event handler for the "+ Add Recipe" button/tap
        private async void OnAddRecipeTapped(object sender, EventArgs e)
        {
            Debug.WriteLine("Add Recipe tapped. Navigating to AddRecipePage.");

            // Create an instance of the AddRecipePage
            var addRecipePage = new AddRecipePage();

            // Subscribe to the RecipeSubmitted event on the AddRecipePage instance
            addRecipePage.RecipeSubmitted += AddRecipePage_RecipeSubmitted;

            // Navigate to the AddRecipePage
            await Navigation.PushAsync(addRecipePage);

            // Optional: Unsubscribe from the event when the page is popped
            // addRecipePage.RecipeSubmitted -= AddRecipePage_RecipeSubmitted; // Could unsubscribe here if needed
        }

        // Event handler for the RecipeSubmitted event from AddRecipePage
        private void AddRecipePage_RecipeSubmitted(object sender, CommunityRecipe newRecipe)
        {
            Debug.WriteLine($"Recipe submitted and received in CommunityFeedPage: {newRecipe.FoodName}");

            // Add the new recipe to the ObservableCollection
            // This will automatically update the CollectionView UI
            // Add it at the beginning of the list to show the newest first
            Recipes.Insert(0, newRecipe);

            // Optional: If you want to scroll to the new item, you can do it here
            // RecipeFeedCollectionView.ScrollTo(newRecipe, position: ScrollToPosition.Start, animate: true);

            // Optional: Unsubscribe from the event here if you didn't do it after PushAsync
            // if (sender is AddRecipePage addPage)
            // {
            //     addPage.RecipeSubmitted -= AddRecipePage_RecipeSubmitted;
            // }
        }

        // Event handler for tapping the Like section
        private void OnLikeTapped(object sender, TappedEventArgs e)
        {
            // Correctly access CommandParameter from the event args itself
            var tappedRecipe = e.Parameter as CommunityRecipe; // Use e.Parameter

            if (tappedRecipe != null)
            {
                Debug.WriteLine($"Like tapped for recipe: {tappedRecipe.FoodName}");
                // TODO: Implement like logic (e.g., increment LikesCount, save to backend)
                // For now, just display a message
                DisplayAlert("Like", $"You liked {tappedRecipe.FoodName}!", "OK");

                // Optional: Increment the count in the UI (requires CommunityRecipe to implement INotifyPropertyChanged)
                // tappedRecipe.LikesCount++;
            }
        }

        // Event handler for tapping the Comment section
        private void OnCommentTapped(object sender, TappedEventArgs e)
        {
            // Correctly access CommandParameter from the event args itself
            var tappedRecipe = e.Parameter as CommunityRecipe; // Use e.Parameter

            if (tappedRecipe != null)
            {
                Debug.WriteLine($"Comment tapped for recipe: {tappedRecipe.FoodName}");
                // TODO: Implement comment logic (e.g., navigate to a comments page, show a comment input)
                // For now, just display a message
                DisplayAlert("Comment", $"You tapped comment for {tappedRecipe.FoodName}!", "OK");
            }
        }

        // The CommunityRecipe class definition should now be in Models/CommunityRecipe.cs
        // REMOVE THIS DUPLICATE DEFINITION IF IT'S STILL HERE!
        /*
        public class CommunityRecipe // <-- REMOVE THIS BLOCK
        {
            // ... properties ...
        }
        */
    }
}
