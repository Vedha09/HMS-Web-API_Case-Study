using AmazeCare.DbContexts;
using AmazeCare.Models;

namespace AmazeCare.Repository.Implementation
{
    public class UserServiceImpl : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserServiceImpl(ApplicationDbContext context) => _context = context;

        public IEnumerable<User> GetAllUsers() => _context.Users.ToList();
        public User GetUserById(int id) => _context.Users.Find(id);
        public void AddUser(User user) { _context.Users.Add(user); _context.SaveChanges(); }
        public void UpdateUser(User user) { _context.Users.Update(user); _context.SaveChanges(); }
        public void DeleteUser(int id) { var u = _context.Users.Find(id); if (u != null) { _context.Users.Remove(u); _context.SaveChanges(); } }
    }
}
