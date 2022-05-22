using HotChocolate.Execution;
using JSONOverHTTP.GraphQL.Data;
using JSONOverHTTP.GraphQL.GraphQLTypes;
using JSONOverHTTP.GraphQL.Models;
using JSONOverHTTP.GraphQL.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var dbPath = System.IO.Path.Join(path, "graphql.db");

builder.Services.AddDbContext<ToDoContext>(opt => opt.UseSqlite($"Data Source={dbPath}"));

//add the services required by Hot Chocolate to DI container
builder.Services
    .AddGraphQLServer()
    .AddType<ToDoItemType>()
    .AddType<ResultType>()
    .AddType<ToDoItemPayloadType>()
    .AddType<ToDoItemInputType>()
    .AddQueryType<QueryType>()
    .AddMutationType<MutationType>()
    .AddSubscriptionType<SubscriptionType>()
     .ModifyOptions(options =>
     {
         options.DefaultResolverStrategy = ExecutionStrategy.Serial;
     });

//resgister services
builder.Services.AddScoped<ToDoItemsRepository>();

builder.Services.AddInMemorySubscriptions();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ToDoContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseWebSockets();

//Adds the endpoint /graphql, Banana Cake Pop is integrated
app.MapGraphQL();

app.Run();


