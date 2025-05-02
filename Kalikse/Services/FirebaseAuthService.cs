using Firebase.Auth;

namespace Kalikse.Services
{
    public class FirebaseAuthService
    {
        private const string ApiKey = "AIzaSyAdTsdHQ7gst3qlnsliVsgOg_LQN-oSu8Q";
        private readonly FirebaseAuthProvider authProvider;

        public FirebaseAuthService()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
        }

        public async Task<FirebaseAuthLink> SignUp(string email, string password)
        {
            return await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
        }

        public async Task<FirebaseAuthLink> SignIn(string email, string password)
        {
            return await authProvider.SignInWithEmailAndPasswordAsync(email, password);
        }

        public async Task<string> GetFreshToken(FirebaseAuthLink authLink)
        {
            await authLink.GetFreshAuthAsync();
            return authLink.FirebaseToken;
        }
    }




}