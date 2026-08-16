using CapstoneApp.Integrations.DbContexts;
using CapstoneApp.Integrations.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CapstoneApp.Endpoints.Endpoints;

public abstract class TodoEndpoints
{
    public static async Task<Results<Ok<List<TodoItem>>, ProblemHttpResult>> GetTodos(
        AppDbContext db,
        ILogger<TodoEndpoints> logger)
    {
        try
        {
            var todos = await db.TodoItems.ToListAsync();
            return TypedResults.Ok(todos);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching todos.");
            return TypedResults.Problem("An unexpected error occurred.");
        }
    }

    public static async Task<Results<Created<TodoItem>, BadRequest<string>, ProblemHttpResult>> CreateTodo(
        TodoItem newItem,
        AppDbContext db,
        ILogger<TodoEndpoints> logger)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(newItem.Title))
                return TypedResults.BadRequest("Title is required.");

            db.TodoItems.Add(newItem);
            await db.SaveChangesAsync();

            return TypedResults.Created($"/api/todos/{newItem.Id}", newItem);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating a todo.");
            return TypedResults.Problem("An unexpected error occurred.");
        }
    }
}