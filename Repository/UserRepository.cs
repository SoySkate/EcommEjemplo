using EcommerceEjemploApi.Data;
using EcommerceEjemploApi.Interfaces;
using EcommerceEjemploApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceEjemploApi.Repository
{
    //funciones que se usaran para acciones CRUD + interface(funciones declaradas)
    //el repository se intercomunica directamente con el context
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUser(User user)
        {
             _context.Add(user);
            return await Save();
        }

        public async Task<bool> DeleteUser(User user)
        {
            _context.Remove(user);
            return await Save();
        }

        public async Task<User> GetUser(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public  async Task<User> GetUserByUsername(string username)
        {
            return _context.Users.Where(u=>u.Name == username).FirstOrDefault();
        }

        public async Task<string> GetUserAddress(int id)
        {
            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            if (user.Address == null) { return "This user have not address"; }

            return user.Address;
        }

        public async Task<bool> UserPhoneExists(string phone)
        {
            return _context.Users.Any(u=>u.Phone==phone);
        }

        public async Task<User> GetUserByPhone(string phone)
        {
            return _context.Users.Where(p => p.Phone == phone).FirstOrDefault();
        }

        public async Task<ICollection<User>> GetUsers()
        {
            return await  _context.Users.OrderBy(u => u.Id).ToListAsync(); ;
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Update(user);
            return await Save();
        }

        public async Task<bool> UserExists(int id)
        {
            return _context.Users.Any(u=> u.Id == id);
        }

        //nose si voy bine xd
        public async Task<User> GetUserByOrderId(int orderId)
        {
            var order = _context.Orders.Where(o=>o.Id == orderId).FirstOrDefault();
            return _context.Users.Where(u => u.Id == order.UserId).FirstOrDefault();
        }
    }
}
