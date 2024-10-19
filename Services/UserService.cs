namespace EcommerceEjemploApi.Services
{
    using EcommerceEjemploApi.Interfaces;
    using EcommerceEjemploApi.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    //Aqui el Serice lo que hace es tener la logica del programa 
    //Se connecta al repositorio y hace las funciones lgicas necesarias
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void RegisterUser(User user, string password)
        {
            // Hash the password
            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            // Store the user in the database
             _userRepository.CreateUser(user);
        }

        public bool VerifyPassword(User user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
