using JSONOverHTTP.GraphQL.Models;

namespace JSONOverHTTP.GraphQL.GraphQLTypes
{
    //map ToDoItem POCO to an object type by inheriting from ObjectType<T>
    public class ToDoItemType : ObjectType<ToDoItem>
    {
        //all public properties and methods are implicitly mapped to the fields on the schema object type
        //behaviour can be overridden
        //types need explicit registration to ensure the correct type is used when we return the object 
    }
}
