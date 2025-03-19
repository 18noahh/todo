using Microsoft.AspNetCore.Mvc;
using ToDo_App_M324;
using Task = ToDo_App_M324.Task;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly TodoList _todoList;

        public WeatherForecastController(TodoList todoList)
        {
            _todoList = todoList;
            _todoList.LoadTasks();
        }

        [HttpGet]
        public ActionResult<List<Task>> GetAllTasks()
        {
            var tasks = _todoList.GetTasks();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<Task> GetTaskById(int id)
        {
            var task = _todoList.GetTasks().FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }
            return Ok(task);
        }

        [HttpPost]
        public ActionResult AddTask([FromBody] Task task)
        {
            if (string.IsNullOrWhiteSpace(task.Name))
            {
                return BadRequest("Task name cannot be empty.");
            }

            var tasks = _todoList.GetTasks();
            task.Id = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
            
            _todoList.AddTask(task);
            _todoList.SaveTasks();

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTask(int id, [FromBody] Task updatedTask)
        {
            var tasks = _todoList.GetTasks();
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            task.Name = updatedTask.Name;
            task.Description = updatedTask.Description;
            task.IsDone = updatedTask.IsDone;

            _todoList.SaveTasks();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            var tasks = _todoList.GetTasks();
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            _todoList.RemoveTask(task);
            _todoList.SaveTasks();

            return NoContent();
        }
    }
}
