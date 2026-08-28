namespace ShoppingAgent.Application.AI
{
    public sealed class ConversationMetadata
    {
        public string? Summary { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
