using Agenda.Domain.Entities;

namespace Agenda.Domain.Interfaces;
public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<bool> ExistsByUsernameAsync(string username);
    Task AddAsync(User user);
}
