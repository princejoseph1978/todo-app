using TodoAppApi.Model;
namespace TodoAppApi.Repository;

/// <summary>
/// In-memory implementation of <see cref="ITodoRepository"/> used for local application storage.
/// </summary>
public class InMemoryTodoRepository : ITodoRepository
{
    private readonly List<TodoItem> _todos = [];

    /// <inheritdoc />
    public IEnumerable<TodoItem> GetAll() => _todos.OrderByDescending(t => t.CreatedAt);

    /// <inheritdoc />
    public TodoItem Add(string title)
    {
        var todo = new TodoItem(Guid.NewGuid(), title, false, DateTime.UtcNow);
        _todos.Add(todo);
        return todo;
    }

    /// <inheritdoc />
    public bool Delete(Guid id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        return todo != null && _todos.Remove(todo);
    }
}
