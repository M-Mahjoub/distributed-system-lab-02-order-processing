using ShoppingAgent.Application.Contracts;
using ShoppingAgent.Application.Conversations;
using ShoppingAgent.Infrastructure;
using ShoppingAgent.Infrastructure.A2A;
using ShoppingAgent.Infrastructure.MCP;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi()
                .InfrastructureDI(builder.Configuration);

var app = builder.Build();

var mcpManager =
    app.Services.GetRequiredService<McpClientManager>();

await mcpManager.ConnectAsync(
    new McpServerConnection(
        "product",
        "http://localhost:3001"));

await mcpManager.ConnectAsync(
    new McpServerConnection(
        "inventory",
        "http://localhost:3002"));

app.MapGet(
    "/mcp/tools",
    async (
        McpClientManager manager,
        CancellationToken cancellationToken) =>
    {
        var tools =
            await manager.GetAllToolsAsync(
                cancellationToken);

        return Results.Ok(
            tools.Select(x => new
            {
                x.Name,
                x.Description
            }));
    });

app.MapGet(
    "/a2a/inventory-test",
    async (
        A2AClientFactory factory,
        CancellationToken cancellationToken) =>
    {
        var client =
            await factory.CreateAsync(
                "http://localhost:4001/",
                cancellationToken);

        //SDK درخواست A2A را به این handler می‌رساند.
        var inventoryClient =
            new InventoryAgentClient(client);

        var result =
            await inventoryClient.SendAsync(
                "Check inventory",
                cancellationToken);

        return Results.Ok(result);
    });

//دو سبک برای ساخت API داریم: Controller-based API (سبک قدیمی‌تر و رایج). 2- Minimal API (از .NET 6 به بعد)
app.MapPost(
    "/chat",
    async (
        ChatRequest request,
        ChatService chatService,
        CancellationToken cancellationToken) =>
    {
        var response =
            await chatService.SendMessageAsync(
                request,
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
