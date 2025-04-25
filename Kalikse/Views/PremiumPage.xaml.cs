using Microsoft.Maui.Controls;
using System.Collections.Generic; // Needed for List<string>

namespace Kalikse
{
    public partial class PremiumPage : ContentPage
    {
        public PremiumPage()
        {
            InitializeComponent();

            // You can optionally set the BindingContext here if needed,
            // but the data is defined directly in XAML using BindableLayout.ItemsSource
        }

        // This class holds the data for each plan item in the XAML
        public class PlanDetail
        {
            public string Title { get; set; }
            public string Price { get; set; }
            // Using IEnumerable<string> is good for BindableLayout
            public IEnumerable<string> Features { get; set; }
            public bool IsPremium { get; set; } // To show the button only for Premium

            // Helper to convert comma-separated string to List<string> for XAML
            public string FeaturesString
            {
                set => Features = value.Split(',').Select(f => f.Trim()).ToList();
            }
        }

        // Event handler for the "Go Premium" button click
        private async void OnGoPremiumClicked(object sender, EventArgs e)
        {
            // TODO: Implement the logic for when the user clicks "Go Premium"
            // This could be showing a payment dialog, navigating to a purchase page, etc.
            await DisplayAlert("Go Premium", "You clicked the Go Premium button!", "OK");
            // Example: await Navigation.PushAsync(new PaymentPage());
        }
    }
}