using Google.Cloud.Firestore;

namespace Deacons.Hybrid.Shared.Firebase
{
    public class FirestoreDbFactory
    {
        private static FirestoreDb _db;
        private static string _projectId;

        public static FirestoreDb GetFirestoreDb(string projectId)
        {
            if (_db == null || _projectId != projectId)
            {
                FirestoreDbBuilder builder = new FirestoreDbBuilder
                {
                    ProjectId = projectId
                };
                _db = builder.Build();
                _projectId = projectId;
            }

            return _db;
        }

    }
}