using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using TodoAppApi.Model;

namespace TodoAppApiTest;

/// <summary>
/// Verifies the end-to-end behavior of the todo API endpoints.
/// </summary>
public class TodoIntegrationTests : IClassFixture<WebApplicationFactory<TodoApiMarker>>
{
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoIntegrationTests"/> class.
    /// </summary>
    /// <param name="factory">The configured web application factory used to create the test client.</param>
    public TodoIntegrationTests(WebApplicationFactory<TodoApiMarker> factory)
    {
        _client = factory.CreateClient();
    }

    /// <summary>
    /// Verifies the full create, read, and delete workflow works through the HTTP API.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task CompleteTodoWorkflow_EndToEnd_Succeeds()
    {
        // 1. Initial State: Verify list starts empty
        var initialResponse = await _client.GetAsync("/api/todos");
        Assert.Equal(HttpStatusCode.OK, initialResponse.StatusCode);

        var initialList = await initialResponse.Content.ReadFromJsonAsync<List<TodoItem>>();
        Assert.NotNull(initialList);
        Assert.Empty(initialList);

        // 2. Add Item: POST a new todo
        var payload = new CreateTodoDto("Integration Test Task");
        var createResponse = await _client.PostAsJsonAsync("/api/todos", payload);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdTodo = await createResponse.Content.ReadFromJsonAsync<TodoItem>();
        Assert.NotNull(createdTodo);
        Assert.Equal("Integration Test Task", createdTodo.Title);

        // 3. Confirm Persisted State: GET items again
        var verifiedResponse = await _client.GetAsync("/api/todos");
        var activeList = await verifiedResponse.Content.ReadFromJsonAsync<List<TodoItem>>();
        Assert.Single(activeList);
        Assert.Equal(createdTodo.Id, activeList[0].Id);

        // 4. Delete Item: Remove the item via DELETE route
        var deleteResponse = await _client.DeleteAsync($"/api/todos/{createdTodo.Id}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // 5. Final State: Verify it's empty again
        var finalResponse = await _client.GetAsync("/api/todos");
        var finalList = await finalResponse.Content.ReadFromJsonAsync<List<TodoItem>>();
        Assert.Empty(finalList);
    }

    /// <summary>
    /// Verifies an empty title is rejected by the API.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task Post_EmptyTitle_ReturnsBadRequest()
    {
        // Arrange
        var payload = new CreateTodoDto("");

        // Act
        var response = await _client.PostAsJsonAsync("/api/todos", payload);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
