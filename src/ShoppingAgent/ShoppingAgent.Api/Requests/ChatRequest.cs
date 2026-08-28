namespace ShoppingAgent.Api.Requests
{
    public sealed record ChatRequest(
     string ConversationId,
     Guid MessageId,
     string Message);
}
