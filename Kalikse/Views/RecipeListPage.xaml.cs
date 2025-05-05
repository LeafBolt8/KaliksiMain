using Kalikse.Models;
using Kalikse.Services;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace Kalikse.Views
{
    public partial class RecipeListPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private ObservableCollection<Recipe> _recipes;

        public RecipeListPage(decimal maxBudget, string dietaryPreference, List<string> allergens)
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            _recipes = new ObservableCollection<Recipe>();
            RecipesCollection.ItemsSource = _recipes;

            LoadFilteredRecipes(maxBudget, dietaryPreference, allergens);
        }

        private async void LoadFilteredRecipes(decimal maxBudget, string dietaryPreference, List<string> allergens)
        {
            try
            {
                var recipes = await _databaseService.GetFilteredRecipesAsync(maxBudget, dietaryPreference, allergens);
                _recipes.Clear();
                foreach (var recipe in recipes)
                {
                    _recipes.Add(recipe);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Failed to load recipes: " + ex.Message, "OK");
            }
        }

        private async void OnRecipeSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Recipe selectedRecipe)
            {
                await Navigation.PushAsync(new RecipeDetailPage(selectedRecipe.Id));
                RecipesCollection.SelectedItem = null;
            }
        }
    }
} 