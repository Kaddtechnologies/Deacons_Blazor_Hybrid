using Google.Cloud.Firestore;

namespace Deacons.Hybrid.Shared.Firebase
{
    public class FirestoreDbService<T>
    {
        private FirestoreDb _db;
        private CollectionReference _collection;

        public FirestoreDbService(string projectId, string collectionName)
        {
            _db = FirestoreDbFactory.GetFirestoreDb(projectId);
            _collection = _db.Collection(collectionName);
        }

        public async Task<List<T>> GetItems()
        {
            QuerySnapshot snapshot = await _collection.GetSnapshotAsync();

            List<T> items = new List<T>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                T item = document.ConvertTo<T>();
                items.Add(item);
            }

            return items;
        }

        public async Task<T> GetItem(Dictionary<string, string> conditions)
        {
            Query query = _collection;

            foreach (var condition in conditions)
            {
                query = query.WhereEqualTo(condition.Key, condition.Value);
            }

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            if (snapshot.Count > 0)
            {
                DocumentSnapshot document = snapshot.Documents[0];
                T item = document.ConvertTo<T>();
                return item;
            }

            return default(T);
        }

        public async Task<DocumentSnapshot> GetItemDocumentSnapshot(Dictionary<string, string> conditions)
        {
            Query query = _collection;

            foreach (var condition in conditions)
            {
                query = query.WhereEqualTo(condition.Key, condition.Value);
            }

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            if (snapshot.Count > 0)
            {
                DocumentSnapshot document = snapshot.Documents[0];
                return document;
            }

            return null;
        }

        public async Task<T> GetItemByDocumentId(string id)
        {
            DocumentReference document = _collection.Document(id);
            DocumentSnapshot snapshot = await document.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                T item = snapshot.ConvertTo<T>();
                return item;
            }

            return default(T);
        }

        public async Task AddItem(T item)
        {
            await _collection.AddAsync(item);
        }

        public async Task UpdateItem(string id, T item)
        {
            DocumentReference document = _collection.Document(id);
            await document.SetAsync(item, SetOptions.Overwrite);
        }

        public async Task DeleteItem(string id)
        {
            DocumentReference document = _collection.Document(id);
            await document.DeleteAsync();
        }
    }
}
