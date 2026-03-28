using Microsoft.AspNetCore.Mvc;
using Agenda.Application.Interfaces;
using Agenda.Application.DTOs;
using Agenda.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Agenda.Api.Controllers;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly IInvitationService _invitationService;

    public EventsController(IEventService eventService, IInvitationService invitationService)
    {
        _eventService = eventService;
        _invitationService = invitationService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Event newEvent)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var result = await _eventService.CreateEventAsync(userId, newEvent);

        if (!result.success)
            return BadRequest(new { message = result.message });

        return Ok(result.@event);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Event @event)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var result = await _eventService.UpdateEventAsync(userId, id, @event);

        if (!result.success) return BadRequest(new { message = result.message });
        return Ok(new { message = result.message });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var result = await _eventService.DeleteEventAsync(userId, id);

        if (!result.success) return BadRequest(new { message = result.message });
        return Ok(new { message = result.message });
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard([FromQuery] DateTime? date, [FromQuery] string? filter)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var dashboardData = await _eventService.GetDashboardDataAsync(userId, date, filter);
        return Ok(dashboardData);
    }

    [HttpPost("{id}/invite")]
    public async Task<IActionResult> Invite(int id, [FromBody] InviteRequest request)
    {
        var senderId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var isValidInvitation = await _invitationService.SendInvitationAsync(id, senderId, request.Username);

        if (!isValidInvitation.isValid) return BadRequest(isValidInvitation.message);
        return Ok(new { message = isValidInvitation.message + request.Username });
    }

    [HttpGet("invitations/pending")]
    public async Task<IActionResult> GetInvitations()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var invitations = await _invitationService.GetPendingInvitationsAsync(userId);
        return Ok(invitations);
    }

    [HttpPost("invitations/{id}/respond")]
    public async Task<IActionResult> Respond(int id, [FromQuery] bool accept)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var success = await _invitationService.RespondToInvitationAsync(id, userId, accept);

        if (!success) return BadRequest("Error al procesar la invitación.");
        return Ok(new { message = accept ? "Evento aceptado" : "Invitación rechazada" });
    }

    [HttpPost("{id}/toggle_status")]
    public async Task<IActionResult> changeStatus(int idEvent)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var isChangeStatus = await _eventService.changeEventStatus(idEvent, userId);

        if(!isChangeStatus.success) return BadRequest(isChangeStatus.message); 

        return Ok(new {message = isChangeStatus.message});
    }
}

