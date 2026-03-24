namespace Agenda.Domain.Entities;
public class EventInvitation
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public int InvitedUserId { get; set; }
    public bool IsAccepted { get; set; } = false;
    
    public Event Event { get; set; } = null!;
}