namespace EcommerceEjemploApi.Services
{
    using AutoMapper;
    using EcommerceEjemploApi.Dto;
    using EcommerceEjemploApi.Interfaces;
    using EcommerceEjemploApi.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    //Aqui el Serice lo que hace es tener la logica del programa 
    //Se connecta al repositorio y hace las funciones lgicas necesarias
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateUser(UserDto user)
        {
            //throw new NotImplementedException();
            //estos comments es lo que tenia en el usercontroller sin el brige de userService
            //if (userCreate == null) { return BadRequest(ModelState); }

            //var users = await _userRepository.GetUsers();
            //var existingUser = users
            //    .Where(u => u.Name.Trim().ToUpper() == userCreate.Name.Trim().ToUpper())
            //    .FirstOrDefault();
            //if (existingUser != null)
            //{
            //    ModelState.AddModelError("", "User Already exists");
            //    return StatusCode(422, ModelState);
            //}
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            //var userMap = _mapper.Map<User>(userCreate);

            //if (!await _userRepository.CreateUser(userMap))
            //{
            //    ModelState.AddModelError("", "Something went wrong while saving");
            //    return StatusCode(500, ModelState);
            //}
            //return Ok("Succesfully created");

            //__________________________this is the usersrvice fun:
            var users = await _userRepository.GetUsers();
            var usersExist = users.Where(u => u.Name.Trim().ToUpper() == user.Name.Trim().ToUpper()).FirstOrDefault();
            if (usersExist != null) { return false; }
            else
            {  // Mapea UserDto a User
                var userMap = _mapper.Map<User>(user);

                // Crea el usuario en el repositorio
                return await _userRepository.CreateUser(userMap);
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _userRepository.GetUser(id);
            if (user == null) { return false; }
            else
            {
                var usermap = _mapper.Map<User>(user);
                return await _userRepository.DeleteUser(usermap);
            }
        }

        public async Task<UserDto> GetUser(int id)
        {
           var userexists = await _userRepository.UserExists(id);
            if (userexists)
            {
                return _mapper.Map<UserDto>(await _userRepository.GetUser(id));
            }

            return null;
         
        }

        public Task<string> GetUserAddress(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetUserByName(string username)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetUserByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetUserByPhone(string phone)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<UserDto>> GetUsers()
        {
           
            return _mapper.Map<List<UserDto>>(await _userRepository.GetUsers());
        }

        public void RegisterUser(User user, string password)
        {
            // Hash the password
            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            // Store the user in the database
             _userRepository.CreateUser(user);
        }

        public async Task<bool> UpdateUser(UserDto user)
        {
            var userExist = _userRepository.UserExists(user.Id);
            if (await userExist)
            {
                var userMap = _mapper.Map<User>(user);
                return await _userRepository.UpdateUser(userMap);
            }
            return false;
        }

        public Task<bool> UserExists(int id)
        {
            return _userRepository.UserExists(id);
        }

        public Task<bool> UserPhoneExists(string phone)
        {
            throw new NotImplementedException();
        }

        public bool VerifyPassword(User user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
