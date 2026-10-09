using OrderMcpServer.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly()
    .WithPromptsFromAssembly()
    .WithResourcesFromAssembly();

builder.Services
    .AddHttpClient<OrderServiceClient>(
        client =>
        {
            client.BaseAddress =
                new Uri(
                    builder.Configuration[
                        "Services:Product"]!);

            client.Timeout =
                TimeSpan.FromSeconds(10);
        })
    .AddStandardResilienceHandler();