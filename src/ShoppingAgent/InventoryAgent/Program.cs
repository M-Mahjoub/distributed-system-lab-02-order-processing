using A2A.AspNetCore;
using InventoryAgent;
using InventoryAgent.MCP;

var builder =
    WebApplication.CreateBuilder(args);

var agentUrl =
    "http://localhost:4001/agent";

//Agent Card در A2A v1 اطلاعاتی مثل version، interface، protocol binding، capabilities، input/output modes و skills را معرفی می‌کند. SDK رسمی فعلی هم همین مدل را استفاده می‌کند.
var agentCard =
    InventoryAgentHandler.GetAgentCard(
        agentUrl);

builder.Services.AddA2AAgent<InventoryAgentHandler>(
    agentCard);

builder.Services.AddSingleton(
    await InventoryMcpClient.CreateAsync(
        "http://localhost:3002"));

var app =
    builder.Build();

app.MapA2A("/agent");

app.MapWellKnownAgentCard(
    agentCard);

app.Run("http://localhost:4001");