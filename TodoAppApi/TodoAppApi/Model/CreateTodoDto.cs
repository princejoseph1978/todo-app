namespace TodoAppApi.Model;

/// <summary>
/// Represents the payload used to create a new todo item.
/// </summary>
/// <param name="Title">The title for the new todo item.</param>
public record CreateTodoDto(string Title);
