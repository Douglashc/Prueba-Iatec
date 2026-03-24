using Microsoft.AspNetCore.Mvc;
using Agenda.Application.Interfaces;
using Agenda.Application.DTOs;

namespace Agenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _authService.LoginAsync(request.Username, request.Password);

        if (!result.success)
            return Unauthorized(new { message = "Credenciales incorrectas" });

        return Ok(new { token = result.token, username = result.username });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);

        if (!result.success)
            return BadRequest(new { message = result.message });

        return Ok(new { message = result.message });
    }
}