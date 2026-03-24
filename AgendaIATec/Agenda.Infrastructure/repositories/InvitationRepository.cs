
using Agenda.Domain.Interfaces;
using Agenda.Domain.Entities;
using Agenda.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infrastructure.repositories;

public class InvitationRepository : IInvitationRepository
{
    private readonly ApplicationDbContext _context;
    public InvitationRepository(ApplicationDbContext context)
    {
        this._context = context;
    }
    public async Task<bool> ExistsAsync(int eventId, int receiverId)
    {
        return await _context.Invitations
            .AnyAsync(i => i.EventId == eventId && i.ReceiverId == receiverId);
    }

    public async Task AddAsync(Invitation invitation)
    {
        _context.Invitations.Add(invitation);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Invitation>> GetPendingInvitationsAsync(int userId)
    {
        return await _context.Invitations
            .Include(i => i.Event)
            .Where(i => i.ReceiverId == userId && i.Status == InvitationStatus.Pending)
            .ToListAsync();
    }

    public async Task<Invitation?> GetByIdAndReceiverAsync(int invitationId, int userId)
    {
        return await _context.Invitations
            .FirstOrDefaultAsync(i => i.Id == invitationId && i.ReceiverId == userId);
    }

    public async Task UpdateAsync(Invitation invitation)
    {
        _context.Invitations.Update(invitation);
        await _context.SaveChangesAsync();
    }
}