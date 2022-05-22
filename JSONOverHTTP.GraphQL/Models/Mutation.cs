using HotChocolate.Subscriptions;
using JSONOverHTTP.GraphQL.Repository;

namespace JSONOverHTTP.GraphQL.Models
{
    public class Mutation
    {
        public ToDoItemPayload AddItem([Service] ToDoItemsRepository toDoItemsRepository, 
            ToDoItemInput item)
        {
            //map objects
            var toDoItem = new ToDoItem
            {
                Name = item.Name,
                Notes = item.Notes,
                Complete = item.Complete,
                DueDate = item.DueDate,
                Priority = item.Priority,
                SendAlert = item.SendAlert
            };

            var added = toDoItemsRepository.AddToDoItem(toDoItem);

            //map objects
            return new ToDoItemPayload()
            {
                Id = added.Id,
                Name = added.Name,
                Notes = added.Notes,
                Complete = added.Complete,
                DueDate = added.DueDate,
                Priority = added.Priority,
                SendAlert = added.SendAlert
            };

        }    

        public ToDoItemPayload UpdateToDoItem([Service] ToDoItemsRepository toDoItemsRepository, ToDoItemInput item, int id)
        {
            var toDoItem = new ToDoItem
            {
                Id =  id,
                Name = item.Name,
                Notes = item.Notes,
                Complete = item.Complete,
                DueDate = item.DueDate,
                Priority = item.Priority,
                SendAlert = item.SendAlert
            };

            var updated = toDoItemsRepository.UpdateToDoItem(toDoItem);

            return new ToDoItemPayload()
            {
                Id = updated.Id,
                Name = updated.Name,
                Notes = updated.Notes,
                Complete = updated.Complete,
                DueDate = updated.DueDate,
                Priority = updated.Priority,
                SendAlert = updated.SendAlert
            };

        }

        public Result DeleteToDoItem([Service] ToDoItemsRepository toDoItemsRepository, int id)
        {
            var updated = toDoItemsRepository.DeleteToDoItem(id);

            return new Result { Message = "Deleted!"};
        }

        public async Task<Result> CompleteToDoItem([Service] ToDoItemsRepository toDoItemsRepository, int id, [Service] ITopicEventSender sender)
        {
            var updated = toDoItemsRepository.MarkComplete(id);

            var todoItem = toDoItemsRepository.GetToDoItemById(id);

            await sender.SendAsync(nameof(Subscription.ItemComplete), todoItem);

            return new Result { Message = "Completed!" };
        }

    }
}
