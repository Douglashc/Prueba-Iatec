using Agenda.Domain.Entities;

namespace Agenda.Domain.Interfaces;

public interface IInvitationRepository
{
    Task<bool> ExistsAsync(int eventId, int receiverId);
    Task AddAsync(Invitation invitation);
    Task<IEnumerable<Invitation>> GetPendingInvitationsAsync(int userId);
    Task<Invitation?> GetByIdAndReceiverAsync(int invitationId, int userId);
    Task UpdateAsync(Invitation invitation);
}