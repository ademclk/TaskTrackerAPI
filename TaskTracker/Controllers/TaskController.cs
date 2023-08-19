using Microsoft.AspNetCore.Mvc;
using TaskTracker.Services.Interfaces;
using TaskTracker.ViewModels.Task;

namespace TaskTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            var tasks = await _taskService.GetAllTasks();
            return Ok(tasks);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Put(CreateRequest request)
        {
            await _taskService.CreateTask(request);

            return Ok(new { message = "Task created" });
        }
    }
}