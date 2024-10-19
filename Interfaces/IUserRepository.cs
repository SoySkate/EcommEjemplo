using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Interfaces
{
    //funciones que hara el repository
    //La interface se comunica directamente con el repository (bueno lo implementa)
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        User GetUser(string username);
        User GetUserByPhone(string phone);
        User GetUserByOrderId(int orderId);
        bool UserPhoneExists(string phone);
        string GetUserAddress(int id);
        bool UserExists(int id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
