using Microsoft.Maui.Controls;
using System;
using System.Diagnostics; // Needed for Debug.WriteLine

namespace Kalikse
{
    public partial class LoginPage : ContentPage
    {
        // Make sure you have x:Name="EmailEntry" and x:Name="PasswordEntry" in your LoginPage.xaml
        // These are defined in the XAML and linked automatically by InitializeComponent()

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnForgotPasswordTapped(object sender, EventArgs e)
        {
            Debug.WriteLine("Forgot Password tapped. Navigating to ForgotPasswordPage.");
            // Navigate to the Forgot Password page
            await Navigation.PushAsync(new ForgotPasswordPage());
        }

        private async void OnSignUpTapped(object sender, EventArgs e)
        {
            Debug.WriteLine("Sign Up tapped. Navigating to RegisterPage.");
            // Navigate to the Register page
            await Navigation.PushAsync(new RegisterPage());
        }

        // Event handler for the "SIGN IN" button
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Sign In button clicked.");

            // Get the text entered by the user
            string enteredEmailOrUsername = EmailEntry.Text?.Trim();
            string enteredPassword = PasswordEntry.Text;

            // Check credentials against the temporary store
            // WARNING: This is NOT secure for a real app!
            // It checks if the entered credentials match the LAST registered credentials.
            if (enteredEmailOrUsername == TemporaryAuthStore.RegisteredEmail && enteredPassword == TemporaryAuthStore.RegisteredPassword)
            {
                // Credentials match - Simulate successful login
                Debug.WriteLine("Login successful.");
                // Navigate to the main part of the app (e.g., MainDashboardPage)
                // Using Application.Current.MainPage replaces the current page stack
                Application.Current.MainPage = new MainDashboardPage();
            }
            else
            {
                // Credentials do NOT match - Show an error message
                Debug.WriteLine("Login failed: Invalid credentials.");
                await DisplayAlert("Login Failed", "Invalid email/username or password.", "OK");
            }
        }
    }
}
