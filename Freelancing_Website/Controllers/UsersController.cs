using AutoMapper;
using CodeSphere.Domain.Interfaces;
using CodeSphere.Domain.Models;
using Freelancing_Website.Models.ForCreate;
using Freelancing_Website.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freelancing_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public UsersController(IRepository<User> userRepository,
        ILogger<UsersController> logger,
        IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var (users,paginationMetaData) = await _userRepository.GetAllAsync(pageNumber,pageSize);
                return Ok(_mapper.Map<List<UserView>>(users));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching users");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                    return NotFound();
                return Ok(_mapper.Map<UserView>(user));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching user with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserForCreate user)
        {
            try
            {
                if (user == null)
                    return BadRequest("User is null");
                var newUser = _mapper.Map<User>(user);
                await _userRepository.AddAsync(newUser);
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id },
                _mapper.Map<UserView>(newUser));
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserForCreate user)
        {
            try
            {
                var oldUser = await _userRepository.GetByIdAsync(id);
                if (oldUser == null)
                    return NotFound();

                oldUser.Name = user.Name;
                oldUser.Email = user.Email;
                oldUser.Rating = user.Rating;
                oldUser.Role = user.Role;

                await _userRepository.UpdateAsync(oldUser);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating user with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting user with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
