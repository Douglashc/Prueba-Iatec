namespace Agenda.Domain.Entities;

public enum InvitationStatus { Pending, Accepted, Rejected }

public class Invitation
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public int ReceiverId { get; set; } // Usuario que recibe la invitación
    public InvitationStatus Status { get; set; } = InvitationStatus.Pending;

    // Propiedades de navegación
    public Event Event { get; set; } = null!;
    public User Receiver { get; set; } = null!;
}