using ShoppingMcpServer.Clients;

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
    .AddHttpClient<ProductServiceClient>(
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
//AddStandardResilienceHandler() یک pipeline استاندارد برای HTTP ایجاد می‌کند که دربرگیرنده سیاست‌هایی مانند:
//Retry
//Timeout
//Circuit Breaker
//Rate Limiting

//است.

builder.Services
    .AddHttpClient<InventoryServiceClient>(
        client =>
        {
            client.BaseAddress =
                new Uri(
                    builder.Configuration[
                        "Services:Inventory"]!);
        })
    .AddStandardResilienceHandler();

var app = builder.Build();

app.MapMcp();

app.Run("http://localhost:3001");

