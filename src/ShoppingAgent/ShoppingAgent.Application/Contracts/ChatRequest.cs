namespace ShoppingAgent.Application.Contracts
{
    public sealed record ChatRequest(
     string ConversationId,
     Guid MessageId,
     string Message);
}
