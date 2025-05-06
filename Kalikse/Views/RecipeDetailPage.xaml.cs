using Kalikse.Models;
using Kalikse.Services;
using System.Collections.ObjectModel;

namespace Kalikse.Views
{
    public partial class RecipeDetailPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly int _recipeId;

        public RecipeDetailPage(int recipeId)
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            _recipeId = recipeId;
            LoadRecipeDetails();
        }

        private async void LoadRecipeDetails()
        {
            try
            {
                var recipe = await _databaseService.GetRecipeByIdAsync(_recipeId);
                if (recipe != null)
                {
                    RecipeImage.Source = recipe.ImageUrl;
                    RecipeName.Text = recipe.Name;
                    PriceRange.Text = $"Total Price Range: ₱{recipe.MinPrice:N2} - ₱{recipe.MaxPrice:N2}";
                    Description.Text = recipe.Description;
                    Instructions.Text = recipe.Instructions;

                    // Create a list of ingredients with formatted price ranges
                    var ingredientsWithPrices = recipe.Ingredients.Select(i => new
                    {
                        Name = i.Name,
                        PriceRange = $"₱{i.MinPrice:N2} - ₱{i.MaxPrice:N2}"
                    }).ToList();

                    IngredientsList.ItemsSource = new ObservableCollection<dynamic>(ingredientsWithPrices);
                    AllergensList.ItemsSource = new ObservableCollection<string>(recipe.Allergens);
                }
                else
                {
                    await DisplayAlert("Error", "Recipe not found", "OK");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Failed to load recipe details: " + ex.Message, "OK");
                await Navigation.PopAsync();
            }
        }
    }
} 