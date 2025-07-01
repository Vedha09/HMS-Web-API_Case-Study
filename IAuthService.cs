using AmazeCare.Models;

namespace AmazeCare.Repository.Implementation
{
    public interface IAuthService
    {
        Task<User> AuthenticateUserAsync(string username, string password);
        string GenerateToken(User user);
    }
}
