namespace Agenda.Application.DTOs;

public class EventResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = string.Empty;
    public int Type { get; set; }
    public int CreatorId { get; set; }
    // Aquí está el cambio clave:
    public List<string> Participants { get; set; } = new();
}