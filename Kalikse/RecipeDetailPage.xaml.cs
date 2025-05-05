using Kalikse.Models;
using static Android.Util.EventLogTags;

namespace Kalikse.Views;

public partial class RecipeDetailPage : ContentPage
{
    private readonly Recipe _recipe;

    public RecipeDetailPage(Recipe recipe)
    {
        InitializeComponent();
        _recipe = recipe;
        LoadRecipeDetails();
    }

    private void LoadRecipeDetails()
    {
        RecipeImage.Source = _recipe.ImagePath;
        RecipeName.Text = _recipe.Name;
        PriceRange.Text = $"Price Range: ₱{_recipe.PriceRangeMin} - ₱{_recipe.PriceRangeMax}";
        Description.Text = _recipe.Description;
        IngredientsList.ItemsSource = _recipe.Ingredients;
    }

    private void OnToolbarItemClicked(object sender, EventArgs e)
    {
        if (Parent is FlyoutPage flyoutPage)
        {
            flyoutPage.IsPresented = true;
        }
    }
}