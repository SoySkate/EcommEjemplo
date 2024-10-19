using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Azure.Core;
using EcommerceEjemploApi.Dto;
using EcommerceEjemploApi.Interfaces;
using EcommerceEjemploApi.Models;
using EcommerceEjemploApi.Repository;
using EcommerceEjemploApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace EcommerceEjemploApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //Aqui el CONTROLLER es la API que se llama a las funciones del Sevice
    //Por ejemplo al crear un user hace el post y llama a la funcion CreateUser from Service
    //((Aqui me he saltado el Service y lo he hecho directo, el controller se conecta directo a la API
    //Cuando la GOODPRACTICE es: que el controller se conecte al service y el service al repositorio))
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IUserService _userService;

        public UserController(IUserRepository userRepository, IMapper mapper, IUserService userService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
        }

        //________________READ ALL Users
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public async Task<IActionResult> GetUsers()
        {
            //var users = _mapper.Map<List<UserDto>>( await _userRepository.GetUsers());
            var users = await _userService.GetUsers();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(users);
        }

        //________________READ A User byID
        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userService.GetUser(userId);
            if (user == null) { return NotFound(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(user);
            //if (! await _userRepository.UserExists(userId)) { return NotFound(); }
            //var user =_mapper.Map<UserDto>( await _userRepository.GetUser(userId));
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState); 
            
            //return Ok(user);
        }

        //________________READ A User by phone(((((not Working)))
        //[HttpGet("{userPhone}")]
        //[ProducesResponseType(200, Type = typeof(User))]
        //[ProducesResponseType(400)]
        //public IActionResult GetUserByPhone(string userPhone)
        //{
        //    if (!_userRepository.UserPhoneExists(userPhone)) { return NotFound(); }
        //    var user = _mapper.Map<UserDto>(_userRepository.GetUserByPhone(userPhone));
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(user);
        //}

        //_______________________________CREATE USER
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userCreate)
        {
            if (userCreate == null) { return BadRequest(ModelState); }

            // Llama al servicio para crear el usuario
            var userCreated = await _userService.CreateUser(userCreate);

            if (!userCreated)
            {
                ModelState.AddModelError("", "User Already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok("Succesfully created");

            //esto es como estaba antes la funcion sin el bridge del UserService::::
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

        }

        //________________________UPDATE USER
        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDto userUpdate)
        {
             if (userUpdate == null) { return BadRequest(ModelState); }

            if (userId != userUpdate.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _userService.UserExists(userId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _userService.UpdateUser(userUpdate))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }
            return NoContent();
            //_______________________________________________________________________________
            //if (userUpdate == null) { return BadRequest(ModelState); }

            //if (userId != userUpdate.Id)
            //{
            //    return BadRequest(ModelState);
            //}
            //if (!await _userRepository.UserExists(userId))
            //    return NotFound();
            //if (!ModelState.IsValid)
            //    return BadRequest();

            //var userMap = _mapper.Map<User>(userUpdate);
            //if (!await _userRepository.UpdateUser(userMap))
            //{
            //    ModelState.AddModelError("", "Something went wrong updating user");
            //    return StatusCode(500, ModelState);
            //}
            //return NoContent();
        }

        //________________________DELETE USER
        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (! await _userService.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _userService.DeleteUser(userId))
                ModelState.AddModelError("", "Something went wrong deleting user");

            return NoContent();
            //_________________________________________________________________
            //if (! await _userRepository.UserExists(userId))
            //    return NotFound();

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            //var userToDelete = await _userRepository.GetUser(userId);
            //if (! await _userRepository.DeleteUser(userToDelete))
            //    ModelState.AddModelError("", "Something went wrong deleting user");

            //return NoContent();
        }

    }
}
