using HotChocolate.Subscriptions;

namespace JSONOverHTTP.GraphQL.Models
{
    public class Subscription
    {
        [Subscribe]
        public ToDoItemPayload ItemComplete([EventMessage]ToDoItem todoItem)
        {
            return new ToDoItemPayload()
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                Notes = todoItem.Notes,
                Complete = todoItem.Complete,
                DueDate = todoItem.DueDate,
                Priority = todoItem.Priority,
                SendAlert = todoItem.SendAlert
            };
        }
    }
}
