using Dapper;
using Dapper.Contrib.Extensions;
using Deacons.Hybrid.Shared.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace Deacons.Hybrid.Shared.Services
{
    public class DapperContrib: IDapperContrib
    {
        private string _connectionString { get; set; }
        public DapperContrib(IConfiguration configuration)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == Microsoft.AspNetCore.Hosting.EnvironmentName.Development)
            {
                _connectionString = configuration.GetConnectionString("DevStaffConnString");
            }
            else
            {
                _connectionString = configuration.GetConnectionString("StaffConnString");
            }
        }

        public async Task<T> Get<T>(Guid id) where T : class, new()
        {
            var component = new T();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                component = await connection.GetAsync<T>(id);
            }

            return component;
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class, new()
        {
            var component = new List<T>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                component = (List<T>)await connection.GetAllAsync<T>();
            }

            return component;
        }

        public async Task<int> Insert<T>(T obj) where T : class, new()
        {
            int component = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    component = await connection.InsertAsync<T>(obj);

                }
                catch (Exception ex)
                {

                    throw;
                }            }

            return component;
        }
        public async Task<bool> InsertorUpdateAsync<T>(T obj) where T : class, new()
        {
            bool component = false;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                if (!await connection.UpdateAsync<T>(obj))
                {
                    await connection.InsertAsync<T>(obj);
                    component = true;
                }
            }

            return component;
        }

        public async Task<bool> Update<T>(T obj) where T : class, new()
        {
            bool component = false;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                component = await connection.UpdateAsync<T>(obj);
            }

            return component;
        }

        public async Task<bool> Delete<T>(T obj) where T : class, new()
        {
            bool component = false;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                component = await connection.DeleteAsync<T>(obj);
            }

            return component;
        }


        public async Task<object?> BatchInsert<T>(IEnumerable<T> list) where T : class, new()
        {
            int component = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                component = await connection.InsertAsync<IEnumerable<T>>(list);
            }

            return component;
        }

       

        

    }


}
