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
            IngredientNameLabel.Text = ingredient.Name;
            if (ingredient.AvailableStore != null)
            {
                CompanyNameLabel.Text = ingredient.AvailableStore.Name;
                CompanyLogoImage.Source = ingredient.AvailableStore.LogoUrl;
                BranchesListView.ItemsSource = ingredient.AvailableStore.Branches;
            }
        }

        private void OnCloseClicked(object sender, EventArgs e)
        {
            Close();
        }
    }
} 