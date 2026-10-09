using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OllamaSharp;
using ShoppingAgent.Application.Abstractions;
using ShoppingAgent.Application.Conversations;
using ShoppingAgent.Application.Memory;
using ShoppingAgent.Application.Services;
using ShoppingAgent.Infrastructure.A2A;
using ShoppingAgent.Infrastructure.AI;
using ShoppingAgent.Infrastructure.MCP;
using ShoppingAgent.Infrastructure.Memory;
using ShoppingAgent.Infrastructure.Repositories;
using ShoppingAgent.Infrastructure.Tools;
using StackExchange.Redis;
using static OllamaSharp.OllamaApiClient;

namespace ShoppingAgent.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection InfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {

            // -------------------------
            // Ollama
            // -------------------------

            var ollama = new OllamaApiClient(
           new Uri("http://localhost:11434"),
           "qwen3:1.7b");

            IChatClient chatClient = ollama;

            services.AddSingleton(chatClient);

            // -------------------------
            // Redis
            // -------------------------


            var redisConnection =
                configuration.GetConnectionString("Redis");

            if (string.IsNullOrWhiteSpace(redisConnection))
            {
                throw new InvalidOperationException(
                    "Redis connection string is not configured.");
            }

            services.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(redisConnection!));

            // -------------------------
            // MCP
            // -------------------------

            services.AddSingleton<McpClientManager>();


            var mcpClient =
                        ShoppingMcpClient.CreateAsync(
                           "http://localhost:3001").Result;

            services.AddSingleton(mcpClient);


            services.AddSingleton<A2AClientFactory>();
            services.AddSingleton<IConversationRepository, RedisConversationRepository>();
            services.AddScoped<IConversationSummarizer, ConversationSummarizer>();
            services.AddScoped<IChatModel, OllamaChatModel>();
            services.AddScoped<ChatService>();
            services.AddScoped<AgentService>();
            services.AddScoped<IConversationSummarizer, ConversationSummarizer>();
            services.AddScoped<IAgentTool, GetCurrentTimeTool>();
            services.AddScoped<ITokenCounter, SimpleTokenCounter>();
            services.AddScoped<IContextManager, ContextManager>();
            services.AddScoped<ILongTermMemory, RedisLongTermMemory>();
            services.AddScoped<IncrementalSummaryService>();
            services.AddScoped<IWorkingMemory, RedisWorkingMemory>();

            return services;
        }
    }
}
