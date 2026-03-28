using Agenda.Domain.Entities;

namespace Agenda.Application.Interfaces;

public interface IEventService
{
    Task<(bool success, string message, Event? @event)> CreateEventAsync(int userId, Event newEvent);
    Task<(bool success, string message)> UpdateEventAsync(int userId, int eventId, Event updatedEvent);
    Task<(bool success, string message)> DeleteEventAsync(int userId, int eventId);
    Task<object> GetDashboardDataAsync(int userId, DateTime? date, string? filter);
    Task<(bool success, string message)> changeEventStatus(int idEvent, int userId);
}