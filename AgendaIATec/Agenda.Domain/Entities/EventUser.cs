namespace Agenda.Domain.Entities;
public class EventUser
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = string.Empty;
    public int Type { get; set; }
    public bool Status { get; set; }
    public int CreatorId { get; set; }
    public List<string> Participants { get; set; } = new();
}