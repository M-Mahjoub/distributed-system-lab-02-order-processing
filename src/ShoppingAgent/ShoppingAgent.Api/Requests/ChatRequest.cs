namespace ShoppingAgent.Api.Requests
{
    public sealed record ChatRequest(
     string ConversationId,
     string Message);
}
