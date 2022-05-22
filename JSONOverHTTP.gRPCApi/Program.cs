using JSONOverHTTP.gRPCApi.Data;
using JSONOverHTTP.gRPCApi.Models;
using JSONOverHTTP.gRPCApi.Repository;
using JSONOverHTTP.gRPCApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc().AddJsonTranscoding();

var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var dbPath = System.IO.Path.Join(path, "gRPCApi.db");

builder.Services.AddDbContext<ToDoContext>(opt => opt.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddScoped<ToDoItemsRepository>();

builder.Services.AddGrpcReflection();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ToDoContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}


// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();

app.MapGrpcService<ToDoService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

IWebHostEnvironment env = app.Environment;
if (env.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.Run();
