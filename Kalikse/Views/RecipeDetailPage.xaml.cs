using Kalikse.Models;
using Kalikse.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using Kalikse.Views;

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
                    var ingredientsWithPrices = recipe.Ingredients.Select(i => new Ingredient
                    {
                        Name = i.Name,
                        MinPrice = i.MinPrice,
                        MaxPrice = i.MaxPrice,
                        AvailableStore = StoreService.GetStoreForIngredient(i.Name)
                    }).ToList();

                    IngredientsList.ItemsSource = new ObservableCollection<Ingredient>(ingredientsWithPrices);
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

        private void OnStoreLogoClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.BindingContext is Ingredient ingredient && ingredient.AvailableStore != null)
            {
                StoreNameLabel.Text = ingredient.AvailableStore.Name;
                BranchesList.ItemsSource = ingredient.AvailableStore.Branches;
                BranchesPopup.IsVisible = true;
            }
        }

        private void OnCloseBranchesPopup(object sender, EventArgs e)
        {
            BranchesPopup.IsVisible = false;
        }

        private async void OnMoreDetailsTapped(object sender, EventArgs e)
        {
            if (sender is Label label && label.BindingContext is Ingredient ingredient && ingredient.AvailableStore != null && !string.IsNullOrEmpty(ingredient.AvailableStore.Name) && ingredient.AvailableStore.Branches != null && ingredient.AvailableStore.Branches.Count > 0)
            {
                var popup = new IngredientDetailsPopup(ingredient);
                await Application.Current.MainPage.ShowPopupAsync(popup);
            }
        }
    }
} 