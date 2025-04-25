// Add this using directive at the top
using Microsoft.Maui.Storage; // Needed for Preferences
using Microsoft.Maui; // Needed for AppTheme
using System.Diagnostics; // Needed for Debug.WriteLine (Good practice for debugging startup)

using Kalikse; // Assuming LoginPage and RegisterPage are in this namespace
using Microsoft.Maui.Controls; // Needed for Application class

namespace Kalikse
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Step 5: Load the saved theme preference on app startup
            LoadSavedThemePreference(); // Call the new method here

            // Embed your LoginPage within a NavigationPage
            // Ensure MainPage is set AFTER loading theme, so the initial page respects the theme
            MainPage = new NavigationPage(new LoginPage());
        }

        // Step 5b: Method to load saved theme preference and apply it on startup
        private void LoadSavedThemePreference()
        {
            // Retrieve the saved preference, default to "System" if not found
            // We use "System" as a string key, mapping to AppTheme.Unspecified
            string savedTheme = Preferences.Get("AppThemePreference", "System");
            Debug.WriteLine($"App startup: Loaded theme preference: {savedTheme}"); // Debug log

            // Apply the theme based on the loaded preference
            if (savedTheme == "Light")
            {
                Application.Current.UserAppTheme = AppTheme.Light;
                Debug.WriteLine("App theme set to Light on startup."); // Debug log
            }
            else if (savedTheme == "Dark")
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
                Debug.WriteLine("App theme set to Dark on startup."); // Debug log
            }
            else // "System" or any other value defaults to Unspecified
            {
                // No explicit theme saved, let the system theme apply
                Application.Current.UserAppTheme = AppTheme.Unspecified; // Unspecified means use system theme
                Debug.WriteLine("App theme set to System (Unspecified) on startup."); // Debug log
            }
        }


        protected override void OnStart()
        {
            // Consider if you need to do anything specific here related to themes
            // Usually, setting it in the constructor is sufficient.
        }

        protected override void OnSleep()
        {
            // Consider saving the current theme here if needed, though
            // Preferences automatically saves immediately when .Set is called.
        }

        protected override void OnResume()
        {
            // Consider if you need to refresh anything based on potential
            // system theme changes while the app was asleep.
            // Application.Current.UserAppTheme = AppTheme.Unspecified; // You could reset to system here if desired
        }
    }
}