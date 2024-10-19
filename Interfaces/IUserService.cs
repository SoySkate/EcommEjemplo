using EcommerceEjemploApi.Dto;
using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Interfaces
{
    //Business logic functions and hight-level operations
    public interface IUserService
    {

        Task<ICollection<UserDto>> GetUsers();
        Task<UserDto> GetUser(int id);
        Task<UserDto> GetUserByName(string username);
        Task<UserDto> GetUserByPhone(string phone);
        Task<UserDto> GetUserByOrderId(int orderId);
        Task<bool> UserPhoneExists(string phone);
        Task<bool> UserExists(int id);
        Task<string> GetUserAddress(int id);
        Task<bool> CreateUser(UserDto user);
        Task<bool> UpdateUser(UserDto user);
        Task<bool> DeleteUser(int userId);
    }
}
