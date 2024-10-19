using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Interfaces
{
    //funciones que hara el repository /(DatabaseAcces)
    //La interface se comunica directamente con el repository (bueno lo implementa)
    public interface IUserRepository
    {
        Task<ICollection<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByPhone(string phone);
        Task<User> GetUserByOrderId(int orderId);
        Task<bool> UserPhoneExists(string phone);
        Task<string> GetUserAddress(int id);
        Task<bool> UserExists(int id);
        Task<bool> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
        Task<bool> Save();
    }
}
