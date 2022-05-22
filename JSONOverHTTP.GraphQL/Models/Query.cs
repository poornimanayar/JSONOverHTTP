using JSONOverHTTP.GraphQL.Repository;

namespace JSONOverHTTP.GraphQL.Models
{
    public class Query
    {
        public List<ToDoItem> GetToDoItems([Service] ToDoItemsRepository toDoItemsRepository)
        {
            return toDoItemsRepository.GetToDoItems();
        }

        public ToDoItem GetToDoItem(int id, [Service] ToDoItemsRepository toDoItemsRepository)
        {
            return toDoItemsRepository.GetToDoItemById(id);
        }

        public List<ToDoItem> GetCompletedToDoItems([Service] ToDoItemsRepository toDoItemsRepository)
        {
            return toDoItemsRepository.GetToDoItemsByStatus(true);
        }

        public List<ToDoItem> GetIncompleteToDoItems([Service] ToDoItemsRepository toDoItemsRepository)
        {
            return toDoItemsRepository.GetToDoItemsByStatus(false);
        }

    }
}
