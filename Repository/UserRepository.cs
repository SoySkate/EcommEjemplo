using EcommerceEjemploApi.Data;
using EcommerceEjemploApi.Interfaces;
using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
             _context.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User GetUser(string username)
        {
            return _context.Users.Where(u=>u.Name == username).FirstOrDefault();
        }

        public string GetUserAddress(int id)
        {
            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            if (user.Address == null) { return "This user have not address"; }

            return user.Address;
        }

        public bool UserPhoneExists(string phone)
        {
            return _context.Users.Any(u=>u.Phone==phone);
        }

        public User GetUserByPhone(string phone)
        {
            return _context.Users.Where(p => p.Phone == phone).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.Id).ToList(); ;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u=> u.Id == id);
        }
    }
}
