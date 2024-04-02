using Deacons.Hybrid.Shared.Models;
using Deacons.Hybrid.Shared.Services.Interface;
using Microsoft.Extensions.Configuration;

namespace Deacons.Hybrid.Shared.Services
{
    public class CalendarEventsService : ICalendarEventsService
    {
        private readonly IDapperContrib _contrib;
        private string _connectionString { get; set; }

        public CalendarEventsService(IDapperContrib dapperContrib, IConfiguration configuration)
        {
            _contrib = dapperContrib;
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

        public async Task<CalendarEventsModel> Get(Guid id)
        {
            return await _contrib.Get<CalendarEventsModel>(id);
        }

        public async Task<IEnumerable<CalendarEventsModel>> GetAll()
        {
            return await _contrib.GetAll<CalendarEventsModel>();
        }

        public async Task<object?> Add(CalendarEventsModel model)
        {
            return await _contrib.Insert<CalendarEventsModel>(model);
        }

        public async Task<object?> Update(CalendarEventsModel model)
        {
            return await _contrib.Update<CalendarEventsModel>(model);
        }

        public async Task<object?> Delete(CalendarEventsModel model)
        {
            return await _contrib.Delete<CalendarEventsModel>(model);
        }
    }
}
