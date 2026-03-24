using Agenda.Domain.Entities;
using Agenda.Application.DTOs;

namespace Agenda.Application.Interfaces;

public interface IAuthService
{
    Task<(bool success, string token, string username)> LoginAsync(string username, string password);
    Task<(bool success, string message)> RegisterAsync(RegisterRequest request);
    string GenerateToken(User user);
    bool VerifyPassword(string password, string hash);
    string HashPassword(string password);
}