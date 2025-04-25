using Microsoft.Maui.Controls;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO; // Needed for Stream
using Microsoft.Maui.Storage; // Needed for FilePicker/MediaPicker
using Kalikse.Models; // Needed for CommunityRecipe

namespace Kalikse
{
    public partial class AddRecipePage : ContentPage
    {
        // Define an event that the CommunityFeedPage can subscribe to
        // This event will be triggered when a recipe is successfully submitted
        public event EventHandler<CommunityRecipe> RecipeSubmitted;

        // Optional: Store the selected image file result here
        private FileResult _selectedImageFile;

        public AddRecipePage()
        {
            InitializeComponent();
        }

        // Event handler for the "Attach Image" button
        private async void OnAttachImageClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Attach Image button clicked.");
            try
            {
                // Use MediaPicker to allow the user to pick a photo
                // Requires adding permissions to your platform projects (Android Manifest, iOS Info.plist)
                _selectedImageFile = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please select a recipe photo"
                });

                if (_selectedImageFile != null)
                {
                    // You can display the image in an Image control if you added one to the XAML
                    // For example, if you added <Image x:Name="RecipeImageView" .../>
                    // RecipeImageView.Source = ImageSource.FromFile(_selectedImageFile.FullPath);
                    Debug.WriteLine($"Image selected: {_selectedImageFile.FullPath}");
                    // Optionally, update the button text or show a checkmark
                    // ((Button)sender).Text = "Image Attached!";
                    await DisplayAlert("Image Selected", $"Selected: {_selectedImageFile.FileName}", "OK"); // Confirmation message
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error picking image: {ex.Message}");
                await DisplayAlert("Error", $"Could not pick image: {ex.Message}", "OK");
            }
        }

        // Event handler for the "SUBMIT RECIPE" button
        private async void OnSubmitRecipeClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Submit Recipe button clicked.");

            // 1. Collect data from the input fields
            string foodName = FoodNameEntry.Text?.Trim();
            string ingredients = IngredientsEditor.Text?.Trim();
            string description = DescriptionEditor.Text?.Trim();

            // 2. Basic Validation
            if (string.IsNullOrEmpty(foodName) || string.IsNullOrEmpty(ingredients) || string.IsNullOrEmpty(description))
            {
                await DisplayAlert("Missing Information", "Please fill in all fields.", "OK");
                Debug.WriteLine("Submission failed: Missing information.");
                return; // Stop if validation fails
            }

            // TODO: In a real app, you would handle image upload here
            // For this showcase, we'll just store the file path or a placeholder
            string imageUrl = _selectedImageFile?.FullPath; // Store the local file path for now
            // WARNING: Using local file paths like this is NOT suitable for sharing across devices/users!

            // 3. Get the registered user's name from the temporary store
            // This will be "Guest User" if no one registered since the app started
            string userName = TemporaryAuthStore.GetRegisteredUserName();

            // 4. Create a new CommunityRecipe object
            var newRecipe = new CommunityRecipe
            {
                UserName = userName, // Use the retrieved user name
                PostDate = DateTime.Now, // Use the current date and time
                FoodName = foodName,
                IngredientsWithPrices = ingredients, // Assuming this format is okay for display
                Description = description,
                ImageUrl = imageUrl, // Assign the image path/URL
                LikesCount = 0, // New recipes start with 0 likes
                CommentsCount = 0 // New recipes start with 0 comments
            };

            // 5. Trigger the RecipeSubmitted event
            // This signals to the CommunityFeedPage that a new recipe is available
            RecipeSubmitted?.Invoke(this, newRecipe);
            Debug.WriteLine("RecipeSubmitted event triggered.");

            // 6. Show confirmation and navigate back
            await DisplayAlert("Success", "Your recipe has been shared!", "OK");

            // Pop this page off the navigation stack to return to the feed
            await Navigation.PopAsync();
            Debug.WriteLine("Navigated back to Community Feed.");
        }
    }
}
