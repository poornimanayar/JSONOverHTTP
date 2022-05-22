using JSONOverHTTP.GraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace JSONOverHTTP.GraphQL.Repository
{
    public class ToDoItemsRepository
    {
        private readonly ToDoContext _context;

        public ToDoItemsRepository(ToDoContext context)
        {
            _context = context;
        }

        public List<ToDoItem> GetToDoItems()
        {
            return _context.ToDoItems.ToList();
        }

        public ToDoItem GetToDoItemById(int id)
        {
            return _context.ToDoItems.FirstOrDefault(u => u.Id == id);
        }

        public bool CheckUserExists(int id)
        {
            return _context.ToDoItems.Any(u => u.Id == id);
        }

        public ToDoItem AddToDoItem(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            _context.SaveChanges();
            return item;
        }

        public ToDoItem UpdateToDoItem(ToDoItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
            var updated = _context.SaveChanges();
            return item;
        }

        public int DeleteToDoItem(int id)
        {
            var item = GetToDoItemById(id);
            _context.ToDoItems.Remove(item);
            var deleted = _context.SaveChanges();
            return deleted;
        }

        public List<ToDoItem> GetToDoItemsByStatus(bool complete)
        {
            return _context.ToDoItems.Where(i => i.Complete == complete).ToList();
        }

        public List<ToDoItem> GetToDoItemsByDueDate(DateTime dueDate)
        {
            return _context.ToDoItems.Where(i => i.DueDate == dueDate).ToList();
        }

        public int MarkComplete(int id)
        {
            var item = GetToDoItemById(id);
            item.Complete = true;
            _context.Entry(item).State = EntityState.Modified;
            var completed = _context.SaveChanges();
            return completed;
        }

    }
}
