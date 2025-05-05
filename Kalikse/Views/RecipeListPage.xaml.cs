using Kalikse.Models;
using Kalikse.Services;
using System.Collections.ObjectModel;

namespace Kalikse.Views
{
    public partial class RecipeListPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private ObservableCollection<Recipe> _recipes;
        private readonly decimal _maxBudget;
        private readonly string _dietaryPreference;
        private readonly List<string> _allergens;

        public RecipeListPage(decimal maxBudget, string dietaryPreference, List<string> allergens)
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            _recipes = new ObservableCollection<Recipe>();
            RecipesCollection.ItemsSource = _recipes;

            _maxBudget = maxBudget;
            _dietaryPreference = dietaryPreference;
            _allergens = allergens;

            UpdateFilterSummary();
            LoadFilteredRecipes(maxBudget, dietaryPreference, allergens);
        }

        private void UpdateFilterSummary()
        {
            var summary = $"Budget: ₱{_maxBudget:N2} • {_dietaryPreference}";
            if (_allergens != null && _allergens.Any())
            {
                summary += $" • Avoiding: {string.Join(", ", _allergens)}";
            }
            FilterSummary.Text = summary;
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