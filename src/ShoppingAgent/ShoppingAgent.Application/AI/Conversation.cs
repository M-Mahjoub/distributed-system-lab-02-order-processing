using ShoppingAgent.Domain;

namespace ShoppingAgent.Application.AI
{
    public sealed class Conversation
    {
        private readonly List<ChatMessageDto> _messages = [];

        public string Id { get; }

        public string? Summary { get; private set; }

        public IReadOnlyCollection<ChatMessageDto> Messages =>
            _messages.AsReadOnly();

        public Conversation(string id)
        {
            Id = id;
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

        public void SetSummary(string summary)
        {
            Summary = summary;
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
        }

        public void AddUserMessage(Guid messageId, string text)
        {
            _messages.Add(
                new ChatMessageDto(
                    messageId,
                    MessageRole.User,
                    new List<ChatContent> {
                        new TextChatContent(text)
                        }));
        }

        public void AddAssistantMessage(string text)
        {
            _messages.Add(
                new ChatMessageDto(
                    Guid.NewGuid(),
                    MessageRole.Assistant,
                    new List<ChatContent> {
                        new TextChatContent(text)
                        }));
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
