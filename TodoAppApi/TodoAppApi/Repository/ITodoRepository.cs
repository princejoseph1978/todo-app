using TodoAppApi.Model;

namespace TodoAppApi.Repository;

/// <summary>
/// Defines the contract for storing and retrieving todo items.
/// </summary>
public interface ITodoRepository
{
    /// <summary>
    /// Gets all todo items ordered by recency.
    /// </summary>
    /// <returns>A collection of todo items.</returns>
    IEnumerable<TodoItem> GetAll();

    /// <summary>
    /// Adds a new todo item with the provided title.
    /// </summary>
    /// <param name="title">The title to assign to the new todo item.</param>
    /// <returns>The newly created todo item.</returns>
    TodoItem Add(string title);

    /// <summary>
    /// Deletes a todo item by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the todo item to remove.</param>
    /// <returns><see langword="true"/> when the item was deleted; otherwise, <see langword="false"/>.</returns>
    bool Delete(Guid id);
}
