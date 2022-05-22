using System.ComponentModel.DataAnnotations.Schema;

namespace JSONOverHTTP.GraphQL.Models
{
    public class ToDoItemPayload
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public bool Complete { get; set; }
        public string Notes { get; set; }
        public int Priority { get; set; }
        public bool SendAlert { get; set; }

    }
}
