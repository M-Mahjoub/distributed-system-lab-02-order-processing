using ShoppingAgent.Api.Requests;
using ShoppingAgent.Application.Abstractions;
using ShoppingAgent.Application.AI;
using ShoppingAgent.Application.Services;
using ShoppingAgent.Domain;
using ShoppingAgent.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi()
                .InfrastructureDI(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

//دو سبک برای ساخت API داریم: Controller-based API (سبک قدیمی‌تر و رایج). 2- Minimal API (از .NET 6 به بعد)
app.MapPost(
    "/chat",
    async (
        ChatRequest request,
        ChatService chatService,
        CancellationToken cancellationToken) =>
    {
        var response =
            await chatService.ChatAsync(
                request.ConversationId,
                request.Message,
                cancellationToken);

        return Results.Ok(
            new
            {
                conversationId =
                    request.ConversationId,

                response
            });
    });

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
