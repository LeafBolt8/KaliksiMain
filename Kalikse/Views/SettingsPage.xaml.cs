// Add these necessary usings at the top
using Microsoft.Maui; // Needed for AppTheme
using Microsoft.Maui.Controls; // For ContentPage, RadioButton etc.
using Microsoft.Maui.Storage; // Needed for Preferences
using System; // Needed for EventArgs
using System.Diagnostics; // Needed for Debug.WriteLine

namespace Kalikse;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        // Step 4a: Set the initial state of the Radio Buttons
        LoadThemePreference();
    }

    // Step 4b: Implement the CheckedChanged event handler
    private void OnThemeRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        // Ensure the event was triggered because a button was checked, not unchecked
        if (e.Value) // e.Value is true if the button is now checked
        {
            var radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                string selectedTheme = radioButton.Content?.ToString();
                Debug.WriteLine($"Theme RadioButton checked: {selectedTheme}"); // Debug log

                if (selectedTheme == "Light")
                {
                    // Set the app theme to Light
                    Application.Current.UserAppTheme = AppTheme.Light;
                    Debug.WriteLine("App theme set to Light."); // Debug log
                    // Save the preference
                    SaveThemePreference("Light");
                }
                else if (selectedTheme == "Dark")
                {
                    // Set the app theme to Dark
                    Application.Current.UserAppTheme = AppTheme.Dark;
                    Debug.WriteLine("App theme set to Dark."); // Debug log
                    // Save the preference
                    SaveThemePreference("Dark");
                }
            }
        }
    }

    // Step 4c: Method to save the theme preference
    private void SaveThemePreference(string theme)
    {
        // Use .NET MAUI Preferences to save a simple key-value pair
        // The key is "AppThemePreference", the value is the theme string ("Light" or "Dark")
        Preferences.Set("AppThemePreference", theme);
        Debug.WriteLine($"Theme preference saved: {theme}"); // Debug log
    }

    // Step 4d: Method to load the saved theme preference and update UI
    private void LoadThemePreference()
    {
        // Retrieve the saved preference, default to "System" if not found
        string savedTheme = Preferences.Get("AppThemePreference", "System");
        Debug.WriteLine($"Loaded theme preference: {savedTheme}"); // Debug log

        // Update the UI (check the correct Radio Button)
        // Also, apply the theme if it was saved as Light or Dark
        if (savedTheme == "Light")
        {
            // Find the "Light" RadioButton and check it
            // We can access controls with x:Name if they are in the code-behind
            // If ThemeOptionsLayout has x:Name, you can find children within it.
            // A simpler way is to iterate or rely on the CheckedChanged event when setting initially.
            // For now, let's rely on the CheckedChanged event being triggered when we set IsChecked below.
            Application.Current.UserAppTheme = AppTheme.Light; // Apply theme immediately
                                                               // Find the radio button by iterating or if you gave them x:Names
            if (ThemeOptionsLayout != null) // Assuming ThemeOptionsLayout has x:Name in XAML
            {
                var lightRadioButton = ThemeOptionsLayout.Children.OfType<RadioButton>().FirstOrDefault(rb => rb.Content?.ToString() == "Light");
                if (lightRadioButton != null)
                {
                    lightRadioButton.IsChecked = true; // This should trigger OnThemeRadioButtonCheckedChanged
                }
            }
        }
        else if (savedTheme == "Dark")
        {
            Application.Current.UserAppTheme = AppTheme.Dark; // Apply theme immediately
            if (ThemeOptionsLayout != null) // Assuming ThemeOptionsLayout has x:Name in XAML
            {
                var darkRadioButton = ThemeOptionsLayout.Children.OfType<RadioButton>().FirstOrDefault(rb => rb.Content?.ToString() == "Dark");
                if (darkRadioButton != null)
                {
                    darkRadioButton.IsChecked = true; // This should trigger OnThemeRadioButtonCheckedChanged
                }
            }
        }
        else // Default or "System"
        {
            // No explicit theme saved, let the system theme apply
            Application.Current.UserAppTheme = AppTheme.Unspecified; // Unspecified means use system theme
            if (ThemeOptionsLayout != null) // Assuming ThemeOptionsLayout has x:Name in XAML
            {
                // Ensure neither Light nor Dark is checked if the preference is System/Unspecified
                var lightRadioButton = ThemeOptionsLayout.Children.OfType<RadioButton>().FirstOrDefault(rb => rb.Content?.ToString() == "Light");
                var darkRadioButton = ThemeOptionsLayout.Children.OfType<RadioButton>().FirstOrDefault(rb => rb.Content?.ToString() == "Dark");
                if (lightRadioButton != null) lightRadioButton.IsChecked = false;
                if (darkRadioButton != null) darkRadioButton.IsChecked = false;

                // You might want a third "System" radio button for this case in the UI
                // For now, we'll just leave both unchecked.
            }
        }
    }
}