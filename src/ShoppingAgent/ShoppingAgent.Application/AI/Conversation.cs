using ShoppingAgent.Domain;

namespace ShoppingAgent.Application.AI
{
    public sealed class Conversation
    {
        private readonly List<ChatMessageDto> _messages = [];

        public string Id { get; }

        public string? Summary { get; private set; }

        public int SummarizedMessageCount { get; private set; }

        public DateTimeOffset CreatedAt { get; }

        public DateTimeOffset UpdatedAt { get; private set; }

        public IReadOnlyCollection<ChatMessageDto> Messages =>
            _messages.AsReadOnly();

        public Conversation(string id)
        {
            Id = id;
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = CreatedAt;
        }

        public Conversation(
            string id,
            IEnumerable<ChatMessageDto> messages,
             string? summary = null)
        {
            Id = id;
            _messages.AddRange(messages);
            Summary = summary;
        }

        public void SetSummary(
        string summary,
        int summarizedMessageCount)
        {
            Summary = summary;
            SummarizedMessageCount =
                summarizedMessageCount;

            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void AddSystemMessage(string text)
        {
            _messages.Add(
                new ChatMessageDto(
                    Guid.NewGuid(),
                    MessageRole.System,
                    new List<ChatContent> {
                        new TextChatContent(text)
                        }
                    ));

            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public ChatMessageDto AddUserMessage(Guid messageId, string text)
        {
            var chatMessage = new ChatMessageDto(
                    messageId,
                    MessageRole.User,
                    new List<ChatContent> {
                        new TextChatContent(text)
                        });
            _messages.Add(chatMessage);

            UpdatedAt = DateTimeOffset.UtcNow;

            return chatMessage;
        }

        public ChatMessageDto AddAssistantMessage(string text)
        {
            var chatMessage = new ChatMessageDto(
                    Guid.NewGuid(),
                    MessageRole.Assistant,
                    new List<ChatContent> {
                        new TextChatContent(text)
                        });
                _messages.Add(chatMessage );
            UpdatedAt = DateTimeOffset.UtcNow;

            return chatMessage;
        }

        public void AddToolCall(
            string callId,
            string name,
            IReadOnlyDictionary<string, object?> arguments)
        {
            _messages.Add(
                new ChatMessageDto(
                    Guid.NewGuid(),
                    MessageRole.Assistant,
                    new List<ChatContent> {
                        new ToolCallChatContent(
                        callId,
                        name,
                        arguments)
                    }));
        }

        public void AddToolResult(
            string callId,
            object? result)
        {
            _messages.Add(
                new ChatMessageDto(
                    Guid.NewGuid(),
                    MessageRole.Tool,
                   new List<ChatContent> {
                        new ToolResultChatContent(
                        callId,
                        result)
                   }));
        }
    }
}
