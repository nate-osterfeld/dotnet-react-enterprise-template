using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using CapstoneApp.Endpoints.Endpoints;

namespace CapstoneApp.Endpoints.Configuration;

public static partial class EndpointMapper
{
	public static void MapTodoEndpoints(this WebApplication app)
	{
		var group = app.MapGroup("/api/todos");

		group.MapGet("/", TodoEndpoints.GetTodos)
			.WithName("GetTodos")
			.WithDescription("Get all todo items");

		group.MapPost("/", TodoEndpoints.CreateTodo)
			.WithName("CreateTodo")
			.WithDescription("Create a new todo item");
	}
}