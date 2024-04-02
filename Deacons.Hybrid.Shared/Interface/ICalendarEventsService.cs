using Deacons.Hybrid.Shared.Models;

namespace Deacons.Hybrid.Shared.Services.Interface
{
    public interface ICalendarEventsService
    {
        Task<CalendarEventsModel> Get(Guid id);

        Task<IEnumerable<CalendarEventsModel>> GetAll();

        Task<object?> Add(CalendarEventsModel model);

        Task<object?> Update(CalendarEventsModel model);

        Task<object?> Delete(CalendarEventsModel model);
    }
}
