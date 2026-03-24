using Agenda.Application.Interfaces;
using Agenda.Domain.Entities;
using Agenda.Domain.Interfaces;

namespace Agenda.Application.Services;
public class InvitationService : IInvitationService
{
    private readonly IUserRepository _userRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IInvitationRepository _invitationRepository;
    public InvitationService(IUserRepository userRepository, IEventRepository eventRepository, IInvitationRepository invitationRepository)
    {
        this._userRepository = userRepository;
        this._eventRepository = eventRepository;
        this._invitationRepository = invitationRepository;
    }

    public async Task<(bool isValid, string message)> SendInvitationAsync(int eventId, int senderId, string receiverUsername)
    {
        var @event = await _eventRepository.GetByIdAndCreatorAsync(eventId, senderId);
        if (@event == null)
            return (false, "Evento no existente o no tiene permiso para realizar esta accion");

        var receiver = await _userRepository.GetByUsernameAsync(receiverUsername);
        if (receiver == null || receiver.Id == senderId)
            return (false, "El destinatario no existe");

        bool exists = await _invitationRepository.ExistsAsync(eventId, receiver.Id);
        if (exists)
            return (false, "El usuario ya tiene una invitacion a este evento");

        var invitation = new Invitation
        {
            EventId = eventId,
            ReceiverId = receiver.Id
        };

        await _invitationRepository.AddAsync(invitation);

        return (true, $"Invitación enviada con éxito a {receiver.Username}");
    }

    public async Task<IEnumerable<Invitation>> GetPendingInvitationsAsync(int userId)
    {
        return await _invitationRepository.GetPendingInvitationsAsync(userId);
    }

    public async Task<bool> RespondToInvitationAsync(int invitationId, int userId, bool accept)
    {
        var inv = await _invitationRepository.GetByIdAndReceiverAsync(invitationId, userId);

        if (inv == null) return false;

        inv.Status = accept
            ? InvitationStatus.Accepted
            : InvitationStatus.Rejected;

        await _invitationRepository.UpdateAsync(inv);

        return true;
    }

}