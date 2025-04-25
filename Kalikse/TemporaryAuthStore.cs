using System;

namespace Kalikse
{
    // WARNING: This is for showcase purposes ONLY.
    // This class stores credentials in memory and is NOT secure or persistent.
    // DO NOT use this in a real application.
    public static class TemporaryAuthStore
    {
        // Static properties to hold the last registered credentials and full name
        public static string RegisteredEmail { get; private set; }
        public static string RegisteredPassword { get; private set; }
        public static string RegisteredFullName { get; private set; } // Added property for Full Name

        // Method to set the registered credentials and full name
        public static void SetCredentials(string fullName, string email, string password) // Added fullName parameter
        {
            RegisteredFullName = fullName; // Store the full name
            RegisteredEmail = email;
            RegisteredPassword = password;
            System.Diagnostics.Debug.WriteLine($"[TemporaryAuthStore] Credentials set for: {RegisteredEmail} (Name: {RegisteredFullName})");
        }

        // Method to clear credentials
        public static void ClearCredentials()
        {
            RegisteredEmail = null;
            RegisteredPassword = null;
            RegisteredFullName = null; // Clear full name as well
            System.Diagnostics.Debug.WriteLine($"[TemporaryAuthStore] Credentials cleared.");
        }

        // Method to check if credentials are set (optional)
        public static bool HasRegisteredUser()
        {
            // Check if email and password are set for a registered user
            return !string.IsNullOrEmpty(RegisteredEmail) && !string.IsNullOrEmpty(RegisteredPassword);
        }

        // Method to get the registered user's name (returns a default if not registered)
        public static string GetRegisteredUserName()
        {
            // Return the stored full name, or a default if no user is registered
            return string.IsNullOrEmpty(RegisteredFullName) ? "Guest User" : RegisteredFullName;
        }
    }
}
