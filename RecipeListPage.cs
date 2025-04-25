namespace Kalikse;

public partial class RecipeListPage : ContentPage
{
    private List<Recipe> _recipes;

    public RecipeListPage(List<Recipe> recipes)
    {
        _recipes = recipes;
        InitializeComponent();
        // Example: Bind the recipes to a UI element like a CollectionView
        // RecipesCollection.ItemsSource = _recipes;
    }
}
