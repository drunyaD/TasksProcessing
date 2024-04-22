using Microsoft.AspNetCore.Mvc;
using TasksProcessing.Logic.DTO;
using TasksProcessing.Logic.Interfaces;
using TasksProcessing.Logic.Requests;

namespace TasksProcessing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_taskService.GetAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _taskService.GetAsync(id);
            if (task is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(task);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TaskDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateTaskRequest createTaskRequest)
        {
            var result = await _taskService.CreateAsync(createTaskRequest);
            return CreatedAtAction(nameof(Post), new { id = result.Id }, result);
        }
    }
}
