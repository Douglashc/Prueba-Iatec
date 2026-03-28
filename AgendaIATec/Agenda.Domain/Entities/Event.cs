namespace Agenda.Domain.Entities;

public enum EventType { Shared, Exclusive }

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = string.Empty;
    public EventType Type { get; set; }
    public bool Status { get; set; } = true;
    public int CreatorId { get; set; }
    public List<int> ParticipantIds { get; set; } = new();

    public bool OverlapsWith(Event other)
    {
        return this.StartDate < other.EndDate && other.StartDate < this.EndDate;
    }
}