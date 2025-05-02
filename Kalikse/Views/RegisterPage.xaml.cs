using Kalikse.Services;
using System.Diagnostics;

namespace Kalikse.Views
{
    public partial class RegisterPage : ContentPage
    {
        private readonly FirebaseAuthService _authService;

        public RegisterPage()
        {
            InitializeComponent();
            _authService = new FirebaseAuthService();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (passwordEntry.Text != confirmPasswordEntry.Text)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            try
            {
                var auth = await _authService.SignUp(emailEntry.Text, passwordEntry.Text);
                string token = await _authService.GetFreshToken(auth);

                await DisplayAlert("Success", $"Account created for {auth.User.Email}", "OK");

                // Navigate to login or dashboard here if you want
                // await Navigation.PushAsync(new LoginPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Registration Failed", ex.Message, "OK");
            }
        }

        private async void OnLoginTapped(object sender, EventArgs e)
        {
            Debug.WriteLine("Login Here tapped. Navigating back to Login Page.");
            // Navigate back to the Login page by popping the current page (RegisterPage) off the stack.
            // This assumes the Login page is the previous page in the navigation stack.
            await Navigation.PopAsync();
        }
    }
}