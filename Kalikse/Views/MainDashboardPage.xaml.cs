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
� � // Partial class definition linked to MainDashboardPage.xaml
� � public partial class MainDashboardPage : FlyoutPage
    {
� � � � // You might have an AuthService here if needed for logout,
        // but the provided snippet doesn't show it.
        // private readonly FirebaseAuthService _authService;

� � � � public MainDashboardPage()
        {
            InitializeComponent();

� � � � � � // Set the initial detail page
� � � � � � // In this case, it's the Dashboard ContentPage defined in the XAML
� � � � � � // The NavigationPage is already set in XAML arguments
            // If you had an AuthService, you might initialize it here:
            // _authService = new FirebaseAuthService();
� � � � }

� � � � // Event handler for the ToolbarItem (Burger Icon)
� � � � // This method toggles the flyout menu
� � � � private void OnToolbarItemClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("ToolbarItem (Burger Icon) clicked.");
� � � � � � // Toggle the IsPresented property to open/close the flyout
� � � � � � IsPresented = !IsPresented;
        }

� � � � // Event handler for selecting an item in the Flyout menu CollectionView
� � � � private async void OnMenuItemSelected(object sender, SelectionChangedEventArgs e)
        {
� � � � � � // Get the selected item (which is a string in this case)
� � � � � � string selectedMenuItem = e.CurrentSelection.FirstOrDefault() as string;

            if (!string.IsNullOrEmpty(selectedMenuItem))
            {
                Debug.WriteLine($"Menu item selected: {selectedMenuItem}");

� � � � � � � � // Reset the selected item in the CollectionView to prevent it from staying highlighted
� � � � � � � � ((CollectionView)sender).SelectedItem = null;

� � � � � � � � // Navigate to the corresponding page based on the selected menu item
� � � � � � � � // We are using the Navigation property of the Detail page's NavigationPage
� � � � � � � � // Ensure the Detail is a NavigationPage in your XAML
� � � � � � � � if (Detail is NavigationPage navigationPage)
                {
� � � � � � � � � � // Close the flyout menu before navigating
� � � � � � � � � � IsPresented = false;

� � � � � � � � � � // Navigate based on the selected item
� � � � � � � � � � switch (selectedMenuItem)
                    {
                        case "Dashboard":
� � � � � � � � � � � � � � // If Dashboard is already the root, no need to push a new instance
� � � � � � � � � � � � � � // You might want to pop to the root if you've navigated away
� � � � � � � � � � � � � � // For this structure, the initial page in x:Arguments is the Dashboard
� � � � � � � � � � � � � � // So navigating back to it might involve popping or resetting the stack
� � � � � � � � � � � � � � // A simple approach is to just ensure the flyout closes.
� � � � � � � � � � � � � � // await navigationPage.PopToRootAsync(); // Use this if you want to clear the stack
� � � � � � � � � � � � � � break;
                        case "Recipes":
� � � � � � � � � � � � � � // TODO: Navigate to your RecipesPage
� � � � � � � � � � � � � � Debug.WriteLine("Navigation to RecipesPage not implemented yet.");
� � � � � � � � � � � � � � // await navigationPage.PushAsync(new RecipesPage());
� � � � � � � � � � � � � � await DisplayAlert("Navigation", "Recipes page not implemented yet.", "OK");
                            break;
                        case "Community":
� � � � � � � � � � � � � � // Navigate to the CommunityFeedPage
� � � � � � � � � � � � � � // Create a new instance of CommunityFeedPage
� � � � � � � � � � � � � � var communityFeedPage = new CommunityFeedPage();
� � � � � � � � � � � � � � // Navigate to the CommunityFeedPage within the Detail's NavigationPage
� � � � � � � � � � � � � � await navigationPage.PushAsync(communityFeedPage);
                            break;
                        case "Profile":
� � � � � � � � � � � � � � // TODO: Navigate to your ProfilePage
� � � � � � � � � � � � � � Debug.WriteLine("Navigation to ProfilePage not implemented yet.");
� � � � � � � � � � � � � � // await navigationPage.PushAsync(new ProfilePage());
� � � � � � � � � � � � � � await DisplayAlert("Navigation", "Profile page not implemented yet.", "OK");
                            break;
                        case "Settings":
� � � � � � � � � � � � � � // TODO: Navigate to your SettingsPage
� � � � � � � � � � � � � � var settingsPage = new SettingsPage();
� � � � � � � � � � � � � � // await navigationPage.PushAsync(new SettingsPage());
� � � � � � � � � � � � � � await navigationPage.PushAsync(settingsPage);
                            break;
                        case "Go Premium":
� � � � � � � � � � � � � � // Navigate to the PremiumPage
� � � � � � � � � � � � � � // Create a new instance of PremiumPage
� � � � � � � � � � � � � � var premiumPage = new PremiumPage();
� � � � � � � � � � � � � � // Navigate to the PremiumPage within the Detail's NavigationPage
� � � � � � � � � � � � � � await navigationPage.PushAsync(premiumPage);
                            break;
                    }
                }
            }
        }

� � � � // Event handler for the Logout button in the Flyout menu
� � � � private async void OnLogoutClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Logout button clicked.");

� � � � � � // TODO: Implement actual logout logic (e.g., clear authentication tokens, etc.)
� � � � � � // If using Firebase Auth, you might call a sign-out method here:
            // await _authService.SignOutAsync(); // Example

� � � � � � // Navigate back to the Login page
� � � � � � // Replacing the Main Page effectively resets the navigation stack
� � � � � � // LoginPage is in the Kalikse.Views namespace, so 'using Kalikse.Views;' is needed at the top
� � � � � � Application.Current.MainPage = new LoginPage(); // This navigates to the Login page
� � � � � � Debug.WriteLine("Navigated back to Login Page.");
        }


� � � � // Event handler for the "Generate Plan" button on the Dashboard
� � � � private async void OnGeneratePlanClicked(object sender, EventArgs e)
        {
            try
            {
                // Get budget
                if (!decimal.TryParse(BudgetEntry.Text, out decimal budget))
                {
                    await DisplayAlert("Error", "Please enter a valid budget amount", "OK");
                    return;
                }

                // Get selected dietary preference
                var dietPref = GetSelectedDietaryPreference();
                if (string.IsNullOrEmpty(dietPref))
                {
                    await DisplayAlert("Error", "Please select a dietary preference", "OK");
                    return;
                }

                // Get selected allergens
                var allergens = GetSelectedAllergens();

                // Navigate to RecipeListPage with the filtered criteria
                if (Detail is NavigationPage navigationPage)
                {
                    await navigationPage.PushAsync(new RecipeListPage(budget, dietPref, allergens));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Failed to generate plan: " + ex.Message, "OK");
            }
        }

        private string GetSelectedDietaryPreference()
        {
            // Find the selected RadioButton in the DietPref group
            var selectedDiet = this.FindByName<RadioButton>("None")?.IsChecked == true ? "None" :
                             this.FindByName<RadioButton>("Keto")?.IsChecked == true ? "Keto" :
                             this.FindByName<RadioButton>("Vegetarian")?.IsChecked == true ? "Vegetarian" :
                             this.FindByName<RadioButton>("Vegan")?.IsChecked == true ? "Vegan" :
                             this.FindByName<RadioButton>("Low Carb")?.IsChecked == true ? "Low Carb" :
                             this.FindByName<RadioButton>("High Protein")?.IsChecked == true ? "High Protein" : null;

            return selectedDiet;
        }

        private List<string> GetSelectedAllergens()
        {
            var allergens = new List<string>();

            if (PeanutsCheckbox.IsChecked)
                allergens.Add("Peanuts");
            if (SeafoodCheckbox.IsChecked)
                allergens.Add("Seafood");
            if (GlutenCheckbox.IsChecked)
                allergens.Add("Gluten");
            if (DairyCheckbox.IsChecked)
                allergens.Add("Dairy");

            return allergens;
        }
    }
}