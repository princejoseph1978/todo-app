namespace TodoAppApi.Model;

/// <summary>
/// Represents a todo item stored by the application.
/// </summary>
/// <param name="Id">The unique identifier for the todo item.</param>
/// <param name="Title">The item title as entered by the user.</param>
/// <param name="IsCompleted">Indicates whether the item has been completed.</param>
/// <param name="CreatedAt">The UTC timestamp when the item was created.</param>
public record TodoItem(Guid Id, string Title, bool IsCompleted, DateTime CreatedAt);
