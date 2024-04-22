using Microsoft.AspNetCore.Mvc;
using TasksProcessing.Logic.DTO;
using TasksProcessing.Logic.Exceptions;
using TasksProcessing.Logic.Interfaces;
using TasksProcessing.Logic.Requests;

namespace TasksProcessing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService taskService)
        {
            _userService = taskService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateUserRequest createUserRequest)
        {
            UserDto result;
            try
            {
                result = await _userService.CreateAsync(createUserRequest);
            }
            catch(LogicException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(Post), new { id = result.Id }, result);
        }
    }
}
