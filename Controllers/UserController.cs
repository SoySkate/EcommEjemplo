using AutoMapper;
using Azure.Core;
using EcommerceEjemploApi.Dto;
using EcommerceEjemploApi.Interfaces;
using EcommerceEjemploApi.Models;
using EcommerceEjemploApi.Repository;
using Microsoft.AspNetCore.Mvc;


namespace EcommerceEjemploApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        //________________READ ALL Users
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());
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
        public IActionResult GetUser(int userId)
        {
            if (!_userRepository.UserExists(userId)) { return NotFound(); }
            var user =_mapper.Map<UserDto>(_userRepository.GetUser(userId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 
            
            return Ok(user);
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
        public IActionResult CreateUser([FromBody] UserDto userCreate)
        {
            if(userCreate == null) { return BadRequest(ModelState); }

            var users = _userRepository.GetUsers()
                .Where(u => u.Name.Trim().ToUpper() == userCreate.Name.Trim().ToUpper())
                .FirstOrDefault();
            if(users != null)
            {
                ModelState.AddModelError("", "User Already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(userCreate);

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }

        //________________________UPDATE USER
        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto userUpdate)
        {
            if(userUpdate == null) { return BadRequest(ModelState); }

            if(userId != userUpdate.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_userRepository.UserExists(userId))
                return NotFound();
            if(!ModelState.IsValid)
                return BadRequest();

            var userMap = _mapper.Map<User>(userUpdate);
            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        //________________________DELETE USER
        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userToDelete = _userRepository.GetUser(userId);
            if (!_userRepository.DeleteUser(userToDelete))
                ModelState.AddModelError("", "Something went wrong deleting user");

            return NoContent();
        }

    }
}
