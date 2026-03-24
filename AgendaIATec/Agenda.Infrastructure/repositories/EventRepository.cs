
using Agenda.Domain.Entities;
using Agenda.Domain.Interfaces;
using Agenda.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infrastructure.repositories;

public class EventRepository : IEventRepository
{
    private readonly ApplicationDbContext _context;
    public EventRepository(ApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task AddAsync(Event @event)
    {
        this._context.Events.Add(@event);
        await this._context.SaveChangesAsync();
    }

    public async Task<bool> ExistsExclusiveOverlapAsync(int userId, DateTime start, DateTime end)
    {
        return await _context.Events.AnyAsync(e =>
            e.CreatorId == userId &&
            e.Type == EventType.Exclusive &&
            start < e.EndDate &&
            e.StartDate < end);
    }

    public async Task<Event?> GetByIdAsync(int eventId)
    {
        return await _context.Events.FindAsync(eventId);
    }

    public async Task<bool> ExistsExclusiveOverlapForUpdateAsync(int userId, int eventId, DateTime start, DateTime end)
    {
        return await _context.Events.AnyAsync(e =>
            e.CreatorId == userId &&
            e.Id != eventId && // ignorar el actual
            e.Type == EventType.Exclusive &&
            start < e.EndDate &&
            e.StartDate < end);
    }

    public async Task UpdateAsync(Event @event)
    {
        _context.Events.Update(@event);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Event @event)
    {
        _context.Events.Remove(@event);
        await _context.SaveChangesAsync();
    }

    public async Task<List<EventUser>> GetCurrentEventsAsync(int userId, DateTime now, DateTime? date, string? filter)
    {
        return await ProjectToDto(BaseQuery(userId, date, filter)
            .Where(e => e.StartDate <= now && e.EndDate >= now))
            .ToListAsync();
    }

    public async Task<List<EventUser>> GetUpcomingEventsAsync(int userId, DateTime now, DateTime? date, string? filter)
    {
        return await ProjectToDto(BaseQuery(userId, date, filter)
            .Where(e => e.StartDate > now)
            .OrderBy(e => e.StartDate)
            .Take(5))
            .ToListAsync();
    }

    public async Task<List<EventUser>> GetOldEventsAsync(int userId, DateTime now, DateTime? date, string? filter)
    {
        return await ProjectToDto(BaseQuery(userId, date, filter)
            .Where(e => e.StartDate < now && e.EndDate < now)
            .OrderByDescending(e => e.StartDate))
            .ToListAsync();
    }

    private IQueryable<Event> BaseQuery(int userId, DateTime? date, string? filter)
    {
        var q = _context.Events.Where(e =>
            e.CreatorId == userId ||
            _context.Invitations.Any(i =>
                i.EventId == e.Id &&
                i.ReceiverId == userId &&
                i.Status == InvitationStatus.Accepted));

        if (date.HasValue)
            q = q.Where(e => e.StartDate.Date == date.Value.Date);

        if (!string.IsNullOrEmpty(filter))
            q = q.Where(e => e.Name.Contains(filter) || e.Description.Contains(filter));

        return q;
    }

    private IQueryable<EventUser> ProjectToDto(IQueryable<Event> query)
    {
        return query.Select(e => new EventUser
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            StartDate = e.StartDate,
            EndDate = e.EndDate,
            Location = e.Location,
            Type = (int)e.Type,
            Participants = _context.Invitations
                .Where(i => i.EventId == e.Id && i.Status == InvitationStatus.Accepted)
                .Select(i => i.Receiver.Username)
                .ToList()
        });
    }

    public async Task<Event?> GetByIdAndCreatorAsync(int eventId, int userId)
    {
        return await _context.Events
            .FirstOrDefaultAsync(e => e.Id == eventId && e.CreatorId == userId);
    }
}