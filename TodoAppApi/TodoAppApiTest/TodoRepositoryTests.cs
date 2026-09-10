namespace TodoAppApiTest;

using TodoAppApi.Repository;
using Xunit;

/// <summary>
/// Verifies the behavior of the in-memory todo repository.
/// </summary>
public class TodoRepositoryTests
{
    /// <summary>
    /// Ensures a valid todo title creates a new todo and stores it in the repository.
    /// </summary>
    [Fact]
    public void Add_ValidTitle_ReturnsNewTodoAndStoresIt()
    {
        // Arrange
        var repo = new InMemoryTodoRepository();
        string expectedTitle = "Write Unit Tests";

        // Act
        var result = repo.Add(expectedTitle);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(expectedTitle, result.Title);
        Assert.False(result.IsCompleted);
        Assert.Single(repo.GetAll());
    }

    /// <summary>
    /// Ensures deleting an existing todo removes it from the collection.
    /// </summary>
    [Fact]
    public void Delete_ExistingId_ReturnsTrueAndRemovesTodo()
    {
        // Arrange
        var repo = new InMemoryTodoRepository();
        var added = repo.Add("Temporary Task");

        // Act
        var deleteResult = repo.Delete(added.Id);
        var remainingItems = repo.GetAll();

        // Assert
        Assert.True(deleteResult);
        Assert.Empty(remainingItems);
    }

    /// <summary>
    /// Ensures deleting a non-existent todo returns false.
    /// </summary>
    [Fact]
    public void Delete_NonExistentId_ReturnsFalse()
    {
        // Arrange
        var repo = new InMemoryTodoRepository();

        // Act
        var deleteResult = repo.Delete(Guid.NewGuid());

        // Assert
        Assert.False(deleteResult);
    }
}

