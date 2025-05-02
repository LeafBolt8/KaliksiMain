using Microsoft.Maui.Controls;
using System;
using System.Diagnostics; // Needed for Debug.WriteLine
using Kalikse.Services; // Needed for FirebaseAuthService
using Kalikse.Views; // Needed to reference other pages like RegisterPage and ForgotPasswordPage
using Firebase.Auth; // Needed for Firebase Authentication types

namespace Kalikse.Views // Ensure this matches the namespace in your LoginPage.xaml
{
    // Partial class definition linked to LoginPage.xaml
    public partial class LoginPage : ContentPage
    {
        // Field to hold the Firebase Authentication Service instance
        private readonly FirebaseAuthService _authService;

        // Constructor for the LoginPage
        public LoginPage()
        {
            // Initialize the UI components defined in LoginPage.xaml
            InitializeComponent();

            // Create an instance of the FirebaseAuthService
            _authService = new FirebaseAuthService();

            // Set the BindingContext if you were using data binding (not strictly needed for this code-behind logic)
            // BindingContext = this;
        }

        // Event handler for the "SIGN IN" button click
        // This method uses the FirebaseAuthService to attempt user sign-in
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                // Attempt to sign in with Firebase Authentication
                var auth = await _authService.SignIn(emailEntry.Text, passwordEntry.Text);
                // Optional: Get a fresh token if needed immediately after login
                // string token = await _authService.GetFreshToken(auth);

                // If SignIn is successful, navigate to the main dashboard
                Debug.WriteLine($"Firebase login successful for {auth.User.Email}. Navigating to Dashboard.");
                // Replace the current Main Page with the Dashboard page
                // This clears the navigation stack and sets the Dashboard as the new root
                Application.Current.MainPage = new MainDashboardPage();


                // You can remove the success DisplayAlert if you navigate immediately
                // await DisplayAlert("Success", $"Welcome {auth.User.Email}", "OK");
            }
            catch (Exception ex)
            {
                // Handle login errors (e.g., invalid credentials, user not found)
                Debug.WriteLine($"Firebase login failed: {ex.Message}");
                await DisplayAlert("Login Failed", ex.Message, "OK");
            }
        }

        // Event handler for the "Forgot Password?" label tap
        // This method navigates to the ForgotPasswordPage
        private async void OnForgotPasswordTapped(object sender, EventArgs e)
        {
            Debug.WriteLine("Forgot Password tapped. Navigating to ForgotPasswordPage.");
            // Navigate to the Forgot Password page using the navigation stack
            // Make sure ForgotPasswordPage exists and is in the Kalikse.Views namespace
            await Navigation.PushAsync(new ForgotPasswordPage());
            Debug.WriteLine("Navigation to ForgotPasswordPage completed.");
        }

        // Event handler for the "Create Account" label tap
        // This method navigates to the RegisterPage
        private async void OnSignUpTapped(object sender, EventArgs e)
        {
            Debug.WriteLine("Create Account tapped. Navigating to RegisterPage.");
            // Navigate to the Register page using the navigation stack
            // Make sure RegisterPage exists and is in the Kalikse.Views namespace
            await Navigation.PushAsync(new RegisterPage());
            Debug.WriteLine("Navigation to RegisterPage completed.");
        }

        // You can add other methods or event handlers below if needed for the LoginPage
    }
}
