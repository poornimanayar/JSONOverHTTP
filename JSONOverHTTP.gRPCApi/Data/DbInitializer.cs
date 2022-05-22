using JSONOverHTTP.gRPCApi.Models;
using System.Text.Json;

namespace JSONOverHTTP.gRPCApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ToDoContext context)
        {

            if (!context.ToDoItems.Any())
            {
                context.ToDoItems.AddRange(SeedToDoItems());
                context.SaveChanges();
            }

        }
        private static List<ToDoItem> SeedToDoItems()
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), @"SeedData\items.json");

            var jsonString = File.ReadAllText(fullPath);

            var items = JsonSerializer.Deserialize<List<ToDoItem>>(jsonString, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return items;
        }


    }
}
