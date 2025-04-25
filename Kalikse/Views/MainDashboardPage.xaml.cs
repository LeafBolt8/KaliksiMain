using Microsoft.Maui.Controls;
using System;
using System.Diagnostics; // Needed for Debug.WriteLine
using Kalikse.Models; // Needed for CommunityRecipe (if used in navigation logic)
using System.Linq; // Needed for .FirstOrDefault()

namespace Kalikse
{
    public partial class MainDashboardPage : FlyoutPage
    {
        public MainDashboardPage()
        {
            InitializeComponent();

            // Set the initial detail page
            // In this case, it's the Dashboard ContentPage defined in the XAML
            // The NavigationPage is already set in XAML arguments
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
            // Get the selected item (which is a string in this case)
            string selectedMenuItem = e.CurrentSelection.FirstOrDefault() as string;

            if (!string.IsNullOrEmpty(selectedMenuItem))
            {
                Debug.WriteLine($"Menu item selected: {selectedMenuItem}");

                // Reset the selected item in the CollectionView to prevent it from staying highlighted
                ((CollectionView)sender).SelectedItem = null;

                // Navigate to the corresponding page based on the selected menu item
                // We are using the Navigation property of the Detail page's NavigationPage
                // Ensure the Detail is a NavigationPage in your XAML
                if (Detail is NavigationPage navigationPage)
                {
                    // Close the flyout menu before navigating
                    IsPresented = false;

                    // Navigate based on the selected item
                    switch (selectedMenuItem)
                    {
                        case "Dashboard":
                            // If Dashboard is already the root, no need to push a new instance
                            // You might want to pop to the root if you've navigated away
                            // For simplicity, we'll just ensure it's the active page if needed
                            if (navigationPage.RootPage.Title != "Dashboard")
                            {
                                // This assumes your initial Detail page is the Dashboard ContentPage
                                // If you navigate deeply, you might need a more robust navigation reset
                                // For this structure, the initial page in x:Arguments is the Dashboard
                                // So navigating back to it might involve popping or resetting the stack
                                // A simple approach is to just ensure the flyout closes.
                                // await navigationPage.PopToRootAsync(); // Use this if you want to clear the stack
                            }
                            break;
                        case "Recipes":
                            // TODO: Navigate to your RecipesPage
                            Debug.WriteLine("Navigation to RecipesPage not implemented yet.");
                            // await navigationPage.PushAsync(new RecipesPage());
                            await DisplayAlert("Navigation", "Recipes page not implemented yet.", "OK");
                            break;
                        case "Community":
                            // Navigate to the CommunityFeedPage
                            // Create a new instance of CommunityFeedPage
                            var communityFeedPage = new CommunityFeedPage();
                            // Navigate to the CommunityFeedPage within the Detail's NavigationPage
                            await navigationPage.PushAsync(communityFeedPage);
                            break;
                        case "Profile":
                            // TODO: Navigate to your ProfilePage
                            Debug.WriteLine("Navigation to ProfilePage not implemented yet.");
                            // await navigationPage.PushAsync(new ProfilePage());
                            await DisplayAlert("Navigation", "Profile page not implemented yet.", "OK");
                            break;
                        case "Settings":
                            // TODO: Navigate to your SettingsPage
                            var settingsPage = new SettingsPage();
                            // await navigationPage.PushAsync(new SettingsPage());
                            await navigationPage.PushAsync(settingsPage);
                            break;
                        case "Go Premium":
                            // Navigate to the PremiumPage
                            // Create a new instance of PremiumPage
                            var premiumPage = new PremiumPage();
                            // Navigate to the PremiumPage within the Detail's NavigationPage
                            await navigationPage.PushAsync(premiumPage);
                            break;
                    }
                }
            }
        }

        // Event handler for the Logout button in the Flyout menu
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Logout button clicked.");

            // TODO: Implement actual logout logic (e.g., clear authentication tokens, etc.)
            // For the temporary local auth, clear the stored credentials
            TemporaryAuthStore.ClearCredentials();
            Debug.WriteLine("Temporary credentials cleared.");


            // Navigate back to the Login page
            // Replacing the Main Page effectively resets the navigation stack
            Application.Current.MainPage = new LoginPage();
            Debug.WriteLine("Navigated back to Login Page.");
        }

        // Event handler for the "Generate Plan" button on the Dashboard
        private async void OnGeneratePlanClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Generate Plan button clicked.");

            // TODO: Implement logic to collect budget, diet preferences, and allergies
            // TODO: Use this information to generate a meal/grocery plan
            // TODO: Display the generated plan (e.g., in a new page or a modal)

            // For now, display a placeholder message
            await DisplayAlert("Generate Plan", "Plan generation logic not implemented yet.", "OK");
        }
    }
}
