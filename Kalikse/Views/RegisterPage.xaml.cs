using Microsoft.Maui.Controls;
using System;
using System.Diagnostics; // Needed for Debug.WriteLine

namespace Kalikse
{
    public partial class RegisterPage : ContentPage
    {
        // These fields are linked to the Entry controls in RegisterPage.xaml
        // because they have matching x:Name properties.
        // Make sure you have x:Name="FullNameEntry", x:Name="RegisterEmailEntry",
        // x:Name="RegisterPasswordEntry", and x:Name="ConfirmPasswordEntry" in your RegisterPage.xaml.

        public RegisterPage()
        {
            InitializeComponent();
        }

        // Event handler for the "CREATE ACCOUNT" button
        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Create Account button clicked.");

            // 1. Collect data from the input fields
            // Access the text from the Entry controls using their x:Name
            string fullName = FullNameEntry.Text?.Trim(); // Get the full name
            string email = RegisterEmailEntry.Text?.Trim();
            string password = RegisterPasswordEntry.Text; // Do not trim password
            string confirmPassword = ConfirmPasswordEntry.Text; // Do not trim password

            // 2. Basic Validation
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                await DisplayAlert("Missing Information", "Please fill in all fields.", "OK");
                Debug.WriteLine("Registration failed: Missing information.");
                return; // Stop if validation fails
            }

            // 3. Check if password and confirm password match
            if (password != confirmPassword)
            {
                await DisplayAlert("Password Mismatch", "Password and Confirm Password do not match.", "OK");
                Debug.WriteLine("Registration failed: Password mismatch.");
                return; // Stop if passwords don't match
            }

            // 4. Simulate registration by saving credentials to the temporary store
            // WARNING: This is NOT secure for a real app!
            TemporaryAuthStore.SetCredentials(fullName, email, password); // Pass the full name here
            Debug.WriteLine($"Simulated registration successful for email: {email} (Name: {fullName})");

            // 5. Show success message and navigate back to Login
            await DisplayAlert("Registration Successful", "Your account has been created. Please log in.", "OK");

            // Navigate back to the Login page
            // PopAsync removes the current page from the navigation stack
            await Navigation.PopAsync();
            Debug.WriteLine("Navigated back to Login Page.");
        }

        // Event handler for the "Login Here" link
        private async void OnLoginTapped(object sender, EventArgs e)
        {
            Debug.WriteLine("Login Here tapped. Navigating back to Login Page.");
            // Navigate back to the Login page
            await Navigation.PopAsync();
        }
    }
}
