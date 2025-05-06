using Kalikse.Models;
using Kalikse.Services;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System.Linq;

namespace Kalikse.Views
{
    public partial class RecipeListPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private ObservableCollection<Recipe> _recipes;
        private string _searchText = "";
        private string _sortBy = "Name";
        private decimal _maxBudget;
        private string _dietaryPreference;
        private List<string> _allergens;

        public RecipeListPage(decimal maxBudget, string dietaryPreference, List<string> allergens)
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            _recipes = new ObservableCollection<Recipe>();
            RecipesCollection.ItemsSource = _recipes;

            _maxBudget = maxBudget;
            _dietaryPreference = dietaryPreference;
            _allergens = allergens;

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

        private async void OnReseedClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Development Reseed", "This will wipe and reseed all recipes. Continue?", "Yes", "No");
            if (!confirm) return;
            await _databaseService.ResetRecipesAsync();
            LoadFilteredRecipes(_maxBudget, _dietaryPreference, _allergens);
            await DisplayAlert("Done", "Recipes have been reseeded.", "OK");
        }

        private async void OnRecipeSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Recipe selectedRecipe)
            {
                await Navigation.PushAsync(new RecipeDetailPage(selectedRecipe.Id));
                RecipesCollection.SelectedItem = null;
            }
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchText = e.NewTextValue;
            ApplySearchAndSort();
        }

        private void OnSortChanged(object sender, EventArgs e)
        {
            _sortBy = SortPicker.SelectedItem?.ToString() ?? "Name";
            ApplySearchAndSort();
        }

        private void ApplySearchAndSort()
        {
            var filtered = _recipes.Where(r => string.IsNullOrEmpty(_searchText) || r.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)).ToList();
            if (_sortBy == "Name")
                filtered = filtered.OrderBy(r => r.Name).ToList();
            else if (_sortBy == "Price")
                filtered = filtered.OrderBy(r => r.MaxPrice).ToList();
            RecipesCollection.ItemsSource = filtered;
        }
    }
} 