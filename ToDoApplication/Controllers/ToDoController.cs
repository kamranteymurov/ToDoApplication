using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Models;

namespace ToDoApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        public IRepository TodoItems { get; set; }
        public ToDoController(IRepository todoItems)
        {
            TodoItems = todoItems;
        }

        //GET:/GetAllTasks/
        [HttpGet]
        public ActionResult<IEnumerable<ToDoTask>> Get()
        {
            var todoItems = TodoItems.GetAll();
            if (todoItems == null || !todoItems.Any())
            {
                return NotFound(new { message = Messages.TaskNotFound }); 
            }
            return Ok(todoItems);
        }

        // GET:/TodoItems/5
        [HttpGet("{id}")]
        public ActionResult<ToDoTask> GetById(long id)
        {
            var todoItems = TodoItems.Find(id);

            if (todoItems == null)
            {
                return NotFound(new { message = Messages.InvalidId });
            }

            return todoItems;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ToDoTask item)
        {
            Messages validationResult = TodoItems.ValidateToDoTask(item);
            if (validationResult.Value != Messages.Success.Value)
            {
                return BadRequest(validationResult);
            }
            TodoItems.Add(item);
            return Ok(new { message = Messages.TaskAddedSuccessfully });
        }
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] ToDoTask item)
        {
            Messages validationResult = TodoItems.ValidateToDoTask(item, "update");
            if (validationResult.Value != Messages.Success.Value)
            {
                return BadRequest(validationResult);
            }
            Messages result = TodoItems.Update(id, item);
            if (result.Value != Messages.Success.Value)
            {
                return BadRequest(result);
            }
            return Ok(new { message = Messages.TaskUpdatedSuccessfully });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todoItem = TodoItems.Find(id);
            if (todoItem == null)
            {
                return NotFound(new { message = Messages.InvalidId});
            }
            if(!todoItem.Status.Equals(StatusTypes.Completed))
            {
                return NotFound(new { message = Messages.UnCompletedTask });
            }
            TodoItems.Remove(id);
            return Ok(new { message = Messages.TaskRemovedSuccessfully });
        }
    }
}