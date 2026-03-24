using Agenda.Domain.Entities;

namespace Agenda.Domain.Interfaces;
public interface IEventRepository
{
    Task AddAsync(Event @event);
    Task<bool> ExistsExclusiveOverlapAsync(int userId, DateTime start, DateTime end);

    //UPDATE
    Task<Event?> GetByIdAsync(int eventId);
    Task<bool> ExistsExclusiveOverlapForUpdateAsync(int userId, int eventId, DateTime start, DateTime end);
    Task UpdateAsync(Event @event);

    //DELETE
    Task DeleteAsync(Event @event);

    //GET EVENTS
    Task<List<EventUser>> GetCurrentEventsAsync(int userId, DateTime now, DateTime? date, string? filter);
    Task<List<EventUser>> GetUpcomingEventsAsync(int userId, DateTime now, DateTime? date, string? filter);
    Task<List<EventUser>> GetOldEventsAsync(int userId, DateTime now, DateTime? date, string? filter);

    //INVITATION
    Task<Event?> GetByIdAndCreatorAsync(int eventId, int userId);
}
