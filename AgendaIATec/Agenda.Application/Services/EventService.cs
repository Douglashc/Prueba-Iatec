using Agenda.Application.Interfaces;
using Agenda.Domain.Entities;
using Agenda.Domain.Interfaces;

namespace Agenda.Application.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<(bool success, string message, Event? @event)> CreateEventAsync(int userId, Event newEvent)
    {
        newEvent.CreatorId = userId;

        if (newEvent.Type == EventType.Exclusive)
        {
            bool overlaps = await _eventRepository.ExistsExclusiveOverlapAsync(
                userId,
                newEvent.StartDate,
                newEvent.EndDate
            );

            if (overlaps)
                return (false, "El evento exclusivo se superpone con otro existente.", null);
        }

        await _eventRepository.AddAsync(newEvent);

        return (true, "Creado con éxito", newEvent);
    }

    public async Task<(bool success, string message)> UpdateEventAsync(int userId, int eventId, Event updatedEvent)
    {
        var existingEvent = await _eventRepository.GetByIdAsync(eventId);

        if (existingEvent == null || existingEvent.CreatorId != userId)
            return (false, "Evento no encontrado o no tienes permiso.");

        if (updatedEvent.Type == EventType.Exclusive)
        {
            bool overlaps = await _eventRepository.ExistsExclusiveOverlapForUpdateAsync(
                userId,
                eventId,
                updatedEvent.StartDate,
                updatedEvent.EndDate
            );

            if (overlaps)
                return (false, "La actualización genera una superposición con otro evento exclusivo.");
        }

        existingEvent.Name = updatedEvent.Name;
        existingEvent.Description = updatedEvent.Description;
        existingEvent.StartDate = updatedEvent.StartDate;
        existingEvent.EndDate = updatedEvent.EndDate;
        existingEvent.Location = updatedEvent.Location;
        existingEvent.Type = updatedEvent.Type;
        existingEvent.Status = updatedEvent.Status;

        await _eventRepository.UpdateAsync(existingEvent);

        return (true, "Evento actualizado con éxito.");
    }

    public async Task<(bool success, string message)> DeleteEventAsync(int userId, int eventId)
    {
        var @event = await _eventRepository.GetByIdAsync(eventId);

        if (@event == null || @event.CreatorId != userId)
            return (false, "No se pudo eliminar el evento.");

        await _eventRepository.DeleteAsync(@event);

        return (true, "Evento eliminado correctamente.");
    }

    public async Task<object> GetDashboardDataAsync(int userId, DateTime? date, string? filter)
    {
        var now = DateTime.Now;

        var currentEvents = await _eventRepository.GetCurrentEventsAsync(userId, now, date, filter);
        var upcomingEvents = await _eventRepository.GetUpcomingEventsAsync(userId, now, date, filter);
        var oldEvents = await _eventRepository.GetOldEventsAsync(userId, now, date, filter);

        return new { currentEvents, upcomingEvents, oldEvents };
    }

    public async Task<(bool success, string message)> changeEventStatus(int idEvent, int userId)
    {
        var existEvent = await this._eventRepository.GetByIdAndCreatorAsync(idEvent, userId);

        if(existEvent == null) return (false, "El evento no fue encontrado");

        existEvent.Status = !existEvent.Status;

        await this._eventRepository.UpdateAsync(existEvent);        

        return (false, "Se actualizo el estado del evento.");
    }
}