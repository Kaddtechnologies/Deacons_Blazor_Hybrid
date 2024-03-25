using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace Deacons.Hybrid.Shared.Firebase
{
    public static class FirebaseInitializer
    {
        public static void Initialize(string credentialPath)
        {
            var  location = Path.Combine(Directory.GetCurrentDirectory(), credentialPath);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", location);

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(location)
            });
        }

        public static FirebaseApp FirebaseInstance => FirebaseApp.DefaultInstance;
    }
}
