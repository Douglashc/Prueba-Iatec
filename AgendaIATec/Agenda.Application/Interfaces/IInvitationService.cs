using Agenda.Domain.Entities;

namespace Agenda.Application.Interfaces;
public interface IInvitationService
{
    Task<(bool isValid, string message)> SendInvitationAsync(int eventId, int senderId, string receiverUsername);
    Task<IEnumerable<Invitation>> GetPendingInvitationsAsync(int userId);
    Task<bool> RespondToInvitationAsync(int invitationId, int userId, bool accept);
}