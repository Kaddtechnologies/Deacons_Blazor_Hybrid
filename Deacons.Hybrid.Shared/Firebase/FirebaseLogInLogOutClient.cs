using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Extensions.Options;

namespace Deacons.Hybrid.Shared.Firebase
{
    public class FirebaseLogInLogOutClient
    {
        private  FirebaseSetting _settings { get; }
        private FirebaseAuthClient Client { get; set; }
        public FirebaseLogInLogOutClient(IOptions<FirebaseSetting> options)
        {
           _settings = options.Value;

            var settings = _settings;                           
            var config = new FirebaseAuthConfig
            {
                ApiKey = settings.APIKey,
                AuthDomain = settings.AuthDomain,
                Providers = new FirebaseAuthProvider[]{
                     new EmailProvider()
                     }
            };
            Client = new FirebaseAuthClient(config);
        }
        public async Task<string?> LoginUserAsync(string username, string password)
        {

            try
            {
                var userCredentials = await Client.SignInWithEmailAndPasswordAsync(username, password);
                return userCredentials is null ? null : await userCredentials.User.GetIdTokenAsync();

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        public void SignOut() => Client.SignOut();

    }

}
