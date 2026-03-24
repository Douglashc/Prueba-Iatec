using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Agenda.Application.Interfaces;
using Agenda.Application.DTOs;
using Agenda.Domain.Entities;
using Agenda.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Agenda.Application.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;

    public AuthService(IConfiguration config, IUserRepository userRepository)
    {
        _config = config;
        this._userRepository = userRepository;
    }

    public async Task<(bool success, string token, string username)> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);

        if (user == null || !VerifyPassword(password, user.Password))
            return (false, "", "");

        var token = GenerateToken(user);

        return (true, token, user.Username);
    }
    public async Task<(bool success, string message)> RegisterAsync(RegisterRequest request)
    {
        bool exists = await _userRepository.ExistsByUsernameAsync(request.Username);

        if (exists)
            return (false, "El usuario ya existe");

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            Password = HashPassword(request.Password),
            Alias = request.Alias
        };

        await _userRepository.AddAsync(user);

        return (true, "Usuario registrado con éxito");
    }

    public string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string HashPassword(string password) =>
        Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

    public bool VerifyPassword(string password, string hash) =>
        HashPassword(password) == hash;
}