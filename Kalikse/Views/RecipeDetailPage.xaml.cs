// === START: Code for RecipeDetailPage.xaml.cs (The List Page Code-Behind) ===
// File: RecipeDetailPage.xaml.cs
// This file now represents the PAGE THAT DISPLAYS THE LIST OF RECIPES.

using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic; // Needed for List<Recipe>
using System.Linq; // Needed for Linq (FirstOrDefault)
using Kalikse.Models; // Import your Models namespace

namespace Kalikse
{
    // RecipeDetailPage will now function as the list page for generated recipes
    public partial class RecipeDetailPage : ContentPage
    {
        // The CollectionView from RecipeDetailPage.xaml *must* have x:Name="RecipesListCollection"
        // for this code to be able to reference it.

        // The constructor now accepts a LIST of Recipe objects
        public RecipeDetailPage(List<Models.Recipe> generatedRecipes)
        {
            InitializeComponent(); // This crucial call connects the code-behind to the XAML and populates the x:Named elements.

            // Set the ItemsSource of the CollectionView (RecipesListCollection) to the list of generated recipes
            // This will populate the list UI with the recipes.
            RecipesListCollection.ItemsSource = generatedRecipes;

            // Optional: Set a BindingContext for the page if your ViewModel (e.g., a new RecipeListViewModel)
            // contains properties or commands needed at the page level (like a command for the toolbar icon).
            // If your toolbar items are using bindings, you would need to set a BindingContext here.
            // Example: BindingContext = new RecipeListViewModel(); // You would need to create this ViewModel if needed
        }

        // --- Add this method to handle recipe selection from the list ---
        // This method is triggered when a user taps an item in the RecipesListCollection.
        // The event handler name "RecipesListCollection_SelectionChanged" MUST match the one in RecipeDetailPage.xaml.
        private async void RecipesListCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected recipe object from the list. CurrentSelection is a list of selected items.
            var selectedRecipe = e.CurrentSelection.FirstOrDefault() as Models.Recipe;

            // If a recipe was actually selected (CurrentSelection is not empty and the item is a Recipe)
            if (selectedRecipe != null)
            {
                // TODO: Navigate to a NEW Recipe Details Page, passing the selectedRecipe object
                // Since RecipeDetailPage is now the list page, you need a *separate* page
                // to show the details (Ingredients, Steps, etc.) of a single recipe.
                // You will need to create a page like Pages/RecipeDetailsInfoPage.xaml/.xaml.cs for this.

                // For now, we'll show an alert to confirm the selection:
                await DisplayAlert("Recipe Selected", $"You selected: {selectedRecipe.Name}", "OK");

                // Example of how you would navigate (UNCOMMENT AND IMPLEMENT ONCE RecipeDetailsInfoPage IS CREATED):
                // Make sure RecipeDetailsInfoPage's constructor accepts a Models.Recipe object
                // var detailsPage = new Pages.RecipeDetailsInfoPage(selectedRecipe);
                // await Navigation.PushAsync(detailsPage);


                // Deselect the item in the CollectionView immediately after handling the selection.
                // This provides better visual feedback to the user and prevents the item
                // from remaining highlighted if they navigate back to this list page.
                ((CollectionView)sender).SelectedItem = null;
            }
        }
        // --- End of selection handling method ---


        // Remove the old static NavigateToRecipeDetailPage method if it's no longer used anywhere else.
        // public static async Task NavigateToRecipeDetailPage(...) { ... }
    }
}
// === END: Code for RecipeDetailPage.xaml.cs ===