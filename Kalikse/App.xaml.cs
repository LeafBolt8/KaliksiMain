// Using directive for the main MAUI namespace
using Microsoft.Maui.Controls;

// Using directive for the root namespace of your project
using Kalikse;

// Using directive for the Views namespace where LoginPage is likely located
using Kalikse.Views; // Add this line

// Using directive for other necessary namespaces if used (e.g., System)
using System;

// Add this using directive at the top
using Microsoft.Maui.Storage; // Needed for Preferences
using Microsoft.Maui; // Needed for AppTheme
using System.Diagnostics; // Needed for Debug.WriteLine (Good practice for debugging startup)
using CommunityToolkit.Maui;

namespace Kalikse // This is the namespace for App.xaml.cs
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Step 5: Load the saved theme preference on app startup
            LoadSavedThemePreference(); // Call the new method here

            // Set the initial page of the application
            // Embed your LoginPage within a NavigationPage
            // This is the correct way to start with navigation capabilities from the Login page.
            MainPage = new NavigationPage(new LoginPage());
            Debug.WriteLine("App started with LoginPage wrapped in NavigationPage.");

            // Ensure MainPage is set AFTER loading theme, so the initial page respects the theme
            // The line above already does this.

            // Note: The global background image logic using a root Grid in App.xaml.cs was incorrect for MainPage.
            // Theme-aware background images are implemented on individual pages using BackgroundImageSource in their XAML files.
            // For example, in LoginPage.xaml, you would use:
            // BackgroundImageSource="{AppThemeBinding Light=light_background.png, Dark=dark_background.png}"

            // Subscribe to the RequestedThemeChanged event if you need to react to system theme changes
            // This is already handled by AppThemeBinding in XAML for colors and can be used for BackgroundImageSource too.
            // You might still need this if you have complex theme logic in code.
            // Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged; // Keep if needed for code logic
        }

        // Optional: Event handler for theme changes (system or UserAppTheme = Unspecified)
        // You might not need this if only using AppThemeBinding in XAML for backgrounds.
        // private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        // {
        //     Debug.WriteLine($"RequestedThemeChanged event fired. New theme: {e.RequestedTheme}");
        //     // If you had theme-specific logic in code, implement it here.
        // }


        // Method to load saved theme preference and apply it on startup
        private void LoadSavedThemePreference()
        {
            // Retrieve the saved preference, default to "System" if not found
            string savedTheme = Preferences.Get("AppThemePreference", "System");
            Debug.WriteLine($"App startup: Loaded theme preference: {savedTheme}"); // Debug log

            // Apply the theme based on the loaded preference
            AppTheme themeToApply = AppTheme.Unspecified; // Default to system

            if (savedTheme == "Light")
            {
                themeToApply = AppTheme.Light;
            }
            else if (savedTheme == "Dark")
            {
                themeToApply = AppTheme.Dark;
            }

            Application.Current.UserAppTheme = themeToApply;
            Debug.WriteLine($"App theme set to {themeToApply} on startup."); // Debug log

            // The background image will be handled by AppThemeBinding in the page XAML.
        }


        protected override void OnStart()
        {
            // Called when the application starts
            Debug.WriteLine("App OnStart");
            // The theme is set in the constructor and LoadSavedThemePreference
        }

        protected override void OnSleep()
        {
            // Called when the application goes to the background
            Debug.WriteLine("App OnSleep");
            // Preferences automatically saves immediately when .Set is called.
        }

        protected override void OnResume()
        {
            // Called when the application resumes from the background
            Debug.WriteLine("App OnResume");
            // If UserAppTheme is Unspecified, the system theme might have changed while asleep.
            // The AppThemeBinding in XAML will automatically react to this.
        }
    }

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            // ... existing code ...
        // ... existing code ...
    }
}



