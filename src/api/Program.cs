var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

// Map endpoints
app.MapPost("/test/hello", HelloHandler.PostTestHello)
    .WithName("PostTestHello")
    .WithOpenApi()
    .Produces(200)
    .Produces(500);

app.Run();

// Make Program public for testing
public partial class Program { }

/// <summary>
/// Handlers for Hello endpoint
/// </summary>
public static class HelloHandler
{
    /// <summary>
    /// POST /test/hello - Returns a simple hello world message
    /// </summary>
    public static IResult PostTestHello()
    {
        try
        {
            return Results.Ok(new { message = "hello world" });
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.Error.WriteLine($"Unhandled exception in /test/hello: {ex.Message}");

            return Results.Json(new
            {
                error = new
                {
                    code = "INTERNAL_SERVER_ERROR",
                    message = "An unexpected error occurred"
                }
            }, statusCode: 500);
        }
    }
}
