using ShoppingAgent.Application.Abstractions;
using ShoppingAgent.Application.AI;
using ShoppingAgent.Domain;

namespace ShoppingAgent.Application.Services
{
    public sealed class ChatService
    {
        //    ChatService
        //│
        //├── Get Conversation from Redis
        //│
        //├── اگر نبود → Create
        //│
        //└── AgentService
        //         │
        //         ├── LLM
        //         ├── Tool
        //         ├── Tool Result
        //         └── Final Answer
        //مدیریت Conversation و ذخیره‌سازی باشد
        private const int SummaryThreshold = 20;

        private readonly IConversationRepository _repository;
        private readonly IChatModel _chatModel;
        private readonly IContextManager _contextManager;
        private readonly IConversationSummarizer _summarizer;
        private readonly IAgentTool _tool;

        public ChatService(
            IConversationRepository repository,
            IChatModel chatModel,
            IContextManager contextManager,
            IConversationSummarizer summarizer,
            IAgentTool tool)
        {
            _repository = repository;
            _chatModel = chatModel;
            _contextManager = contextManager;
            _summarizer = summarizer;
            _tool = tool;
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
            }

            conversation.AddUserMessage(message);

            var context =
                _contextManager.BuildContext(
                    conversation);

            var response =
                await _chatModel.GenerateAsync(
                    context,
                  new List<IAgentTool> { _tool },
                  cancellationToken: cancellationToken);

            conversation.AddAssistantMessage(response.Text);

            if (conversation.Messages.Count >=
                SummaryThreshold)
            {
                var messagesToSummarize =
                    conversation.Messages
                        .Where(x =>
                            x.Role != MessageRole.System)
                        .ToList();

                var summary =
                    await _summarizer.SummarizeAsync(
                        messagesToSummarize,
                        cancellationToken);

                conversation.SetSummary(summary);
            }

            await _repository.ReplaceAsync(
                conversation,
                cancellationToken);

            return response.Text;
        }
    }
}
