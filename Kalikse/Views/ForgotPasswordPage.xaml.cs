namespace Kalikse;

public partial class ForgotPasswordPage : ContentPage
{
    public ForgotPasswordPage()
    {
        InitializeComponent();
    }

    private async void OnSendClicked(object sender, EventArgs e)
    {
        // Add actual email sending logic
        await DisplayAlert("Success", "Reset link sent to your email", "OK");
        await Navigation.PopAsync();
    }

    private async void OnLoginTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}