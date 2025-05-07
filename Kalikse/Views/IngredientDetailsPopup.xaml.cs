using CommunityToolkit.Maui.Views;
using Kalikse.Models;
using System.Collections.Generic;

namespace Kalikse.Views
{
    public partial class IngredientDetailsPopup : Popup
    {
        public IngredientDetailsPopup(Ingredient ingredient)
        {
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine($"DEBUG: Ingredient={ingredient.Name}, Store={ingredient.AvailableStore?.Name}, BranchesCount={ingredient.AvailableStore?.Branches?.Count}");
            IngredientNameLabel.Text = ingredient.Name;
            if (ingredient.AvailableStore != null && !string.IsNullOrEmpty(ingredient.AvailableStore.Name) && ingredient.AvailableStore.Branches != null && ingredient.AvailableStore.Branches.Count > 0)
            {
                CompanyNameLabel.Text = ingredient.AvailableStore.Name;
                CompanyLogoImage.Source = ingredient.AvailableStore.LogoUrl;
                BranchesListView.ItemsSource = ingredient.AvailableStore.Branches;
            }
            else
            {
                Close(); // Close immediately if no valid company/branches
            }
        }

        private void OnCloseClicked(object sender, EventArgs e)
        {
            Close();
        }
    }
} 