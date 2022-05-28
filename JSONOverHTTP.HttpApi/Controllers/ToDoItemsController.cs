using Microsoft.AspNetCore.Mvc;
using JSONOverHTTP.HttpApi.Models;
using JSONOverHTTP.HttpApi.Repository;

namespace JSONOverHTTP.HttpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly ToDoItemsRepository _toDoItemsRepository;

        public ToDoItemsController(ToDoItemsRepository toDoItemsRepository)
        {
            _toDoItemsRepository = toDoItemsRepository;
        }


        // GET: api/ToDoItems
        [HttpGet]
        public ActionResult<IEnumerable<ToDoItem>> GetToDoItems()
        {

            return _toDoItemsRepository.GetToDoItems();
        }

        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        public ActionResult<ToDoItem> GetToDoItem(long id)
        {
          
            var toDoItem = _toDoItemsRepository.GetToDoItemById(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return toDoItem;
        }

        // PUT: api/ToDoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutToDoItem(long id, ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return BadRequest();
            }

            _toDoItemsRepository.UpdateToDoItem(toDoItem);

            return NoContent();
        }

        // POST: api/ToDoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<ToDoItem> PostToDoItem(ToDoItem toDoItem)
        {
          if (toDoItem == null)
          {
              return Problem("ToDoItem is null.");
          }
            _toDoItemsRepository.AddToDoItem(toDoItem);

            ///returns 201, adds location header that specifies the url of the newly added item
            return CreatedAtAction(nameof(GetToDoItem), new { id = toDoItem.Id }, toDoItem);
        }

        // DELETE: api/ToDoItems/5
        [HttpDelete("{id}")]
        public IActionResult DeleteToDoItem(long id)
        {
            _toDoItemsRepository.DeleteToDoItem(id);

            return NoContent();
        }

    }
}
