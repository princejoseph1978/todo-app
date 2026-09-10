using TodoAppApi.Model;
using TodoAppApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Register dependencies for clean architecture & unit testing
builder.Services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();

// Configure CORS for Angular local development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("AngularClient");

// Route Endpoints
app.MapGet("/api/todos", (ITodoRepository repo) =>
    Results.Ok(repo.GetAll()));

app.MapPost("/api/todos", (CreateTodoDto dto, ITodoRepository repo) =>
{
    if (string.IsNullOrWhiteSpace(dto.Title))
        return Results.BadRequest("Title cannot be empty.");

    var todo = repo.Add(dto.Title);
    return Results.Created($"/api/todos/{todo.Id}", todo);
});

app.MapDelete("/api/todos/{id:guid}", (Guid id, ITodoRepository repo) =>
    repo.Delete(id) ? Results.NoContent() : Results.NotFound());

app.Run();

/// <summary>
/// Marker type used by the ASP.NET Core test host to identify the API application.
/// </summary>
public class TodoApiMarker { }