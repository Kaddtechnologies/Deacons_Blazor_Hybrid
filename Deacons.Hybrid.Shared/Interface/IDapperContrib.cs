namespace Deacons.Hybrid.Shared.Services.Interface
{
    public interface IDapperContrib
    {
        Task<T> Get<T>(Guid id) where T : class, new();
        Task<IEnumerable<T>> GetAll<T>() where T : class, new();
        Task<int> Insert<T>(T obj) where T : class, new();
        Task<object?> BatchInsert<T>(IEnumerable<T> list) where T : class, new();
        Task<bool> Update<T>(T obj) where T : class, new();
        Task<bool> Delete<T>(T obj) where T : class, new();

    }
}
