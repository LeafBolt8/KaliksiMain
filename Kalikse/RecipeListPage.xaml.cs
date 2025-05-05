using System.Collections.ObjectModel;
using Kalikse.Models;
using Kalikse.Data;

namespace Kalikse.Views;

public partial class RecipeListPage : ContentPage
{
    private readonly DatabaseService _databaseService;
    private readonly decimal _userBudget;
    private readonly string _dietaryPreference;
    private readonly List<string> _allergies;

    public RecipeListPage(decimal userBudget, string dietaryPreference, List<string> allergies)
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
        _userBudget = userBudget;
        _dietaryPreference = dietaryPreference;
        _allergies = allergies;
        LoadFilteredRecipes();
    }

    private async void LoadFilteredRecipes()
    {
        try
        {
            var recipes = await _databaseService.GetFilteredRecipesAsync(_userBudget, _dietaryPreference, _allergies);
            RecipesCollection.ItemsSource = recipes;
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
            await Navigation.PushAsync(new RecipeDetailPage(selectedRecipe));
        }
    }

    private void OnToolbarItemClicked(object sender, EventArgs e)
    {
        if (Parent is FlyoutPage flyoutPage)
        {
            flyoutPage.IsPresented = true;
        }
    }
}