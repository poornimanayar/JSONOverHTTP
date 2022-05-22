
namespace JSONOverHTTP.GraphQL.Models
{
    public class ToDoItemInput
    {
         public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public bool Complete { get; set; }
        public string Notes { get; set; }
        public int Priority { get; set; }
        public bool SendAlert { get; set; }

    }
}
