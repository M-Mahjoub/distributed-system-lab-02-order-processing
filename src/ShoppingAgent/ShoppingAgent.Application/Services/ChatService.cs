using ShoppingAgent.Application.Abstractions;
using ShoppingAgent.Application.AI;

namespace ShoppingAgent.Application.Services
{
    public sealed class ChatService
    {
        private readonly IConversationRepository _repository;
        private readonly AgentService _agentService;

        public ChatService(
            IConversationRepository repository,
            AgentService agentService)
        {
            _repository = repository;
            _agentService = agentService;
        }

        public async Task<string> ChatAsync(
            string conversationId,
            string message,
            CancellationToken cancellationToken = default)
        {
            var conversation =
                await _repository.GetAsync(
                    conversationId,
                    cancellationToken);

            if (conversation is null)
            {
                conversation =
                    new Conversation(conversationId);

                conversation.AddSystemMessage(
                    "You are a helpful shopping assistant.");

                await _repository.CreateAsync(
                    conversation,
                    cancellationToken);
            }

            var response =
                await _agentService.RunAsync(
                    conversation,
                    message,
                    cancellationToken);

            // در این مرحله Conversation
            // شامل User/Tool/Assistant messages است.
            await _repository.ReplaceAsync(
                conversation,
                cancellationToken);

            return response;
        }
    }
}
