using Microsoft.Maui.Controls;
using System;
using System.Diagnostics; // Needed for Debug.WriteLine
using Kalikse.Services; // Needed for FirebaseAuthService
using Kalikse.Views; // Needed to reference other pages like RegisterPage, ForgotPasswordPage, and LoginPage
using Firebase.Auth; // Needed for Firebase Authentication types
using System.Linq; // Needed for .FirstOrDefault()
using System.Collections.Generic;

// Corrected Namespace to match x:Class in MainDashboardPage.xaml
namespace Kalikse
{
    // Partial class definition linked to MainDashboardPage.xaml
    public partial class MainDashboardPage : FlyoutPage
    {
        // You might have an AuthService here if needed for logout,
        // but the provided snippet doesn't show it.
        // private readonly FirebaseAuthService _authService;

        public MainDashboardPage()
        {
            InitializeComponent();

            // Set the initial detail page
            // In this case, it's the Dashboard ContentPage defined in the XAML
            // The NavigationPage is already set in XAML arguments
            // If you had an AuthService, you might initialize it here:
            // _authService = new FirebaseAuthService();
        }

        // Event handler for the ToolbarItem (Burger Icon)
        // This method toggles the flyout menu
        private void OnToolbarItemClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("ToolbarItem (Burger Icon) clicked.");
            // Toggle the IsPresented property to open/close the flyout
            IsPresented = !IsPresented;
        }

        // Event handler for selecting an item in the Flyout menu CollectionView
        private async void OnMenuItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is string selectedItem)
            {
                switch (selectedItem)
                {
                    case "Dashboard":
                        // Already on dashboard
                        break;
                    case "Recipes":
                        Navigation.PushAsync(new RecipeListPage(0, "None", new List<string>()));
                        break;
                    case "Community":
                        // Navigate to community page
                        break;
                    case "Profile":
                        // Navigate to profile page
                        break;
                    case "Settings":
                        // Navigate to settings page
                        break;
                    case "Go Premium":
                        // Navigate to premium page
                        break;
                }
            }
        }

        // Event handler for the Logout button in the Flyout menu
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Logout button clicked.");

            // TODO: Implement actual logout logic (e.g., clear authentication tokens, etc.)
            // If using Firebase Auth, you might call a sign-out method here:
            // await _authService.SignOutAsync(); // Example

            // Navigate back to the Login page
            // Replacing the Main Page effectively resets the navigation stack
            // LoginPage is in the Kalikse.Views namespace, so 'using Kalikse.Views;' is needed at the top
            Application.Current.MainPage = new LoginPage(); // This navigates to the Login page
            Debug.WriteLine("Navigated back to Login Page.");
        }

        // Event handler for the "Generate Plan" button on the Dashboard
        private async void OnGeneratePlanClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Generate Plan button clicked.");

            // Get user inputs
            if (!decimal.TryParse(BudgetEntry.Text, out decimal budget))
            {
                await DisplayAlert("Error", "Please enter a valid budget amount", "OK");
                return;
            }

            // Get selected dietary preference
            string dietaryPreference = "None";
            if (FindByName("DietPref") is RadioButton selectedRadio)
            {
                var parent = selectedRadio.Parent as Grid;
                if (parent != null)
                {
                    var label = parent.Children.OfType<Label>().FirstOrDefault();
                    if (label != null)
                    {
                        dietaryPreference = label.Text;
                    }
                }
            }

            // Get selected allergies
            var allergies = new List<string>();
            if (PeanutsCheckbox.IsChecked) allergies.Add("Peanuts");
            // Add other allergy checkboxes here

            // Navigate to recipe list page with filters
            await Navigation.PushAsync(new RecipeListPage(budget, dietaryPreference, allergies));
        }
    }
}
