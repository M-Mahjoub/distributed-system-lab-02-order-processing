using ShoppingAgent.Application.Abstractions;
using ShoppingAgent.Application.AI;
using ShoppingAgent.Domain;

public sealed class IncrementalSummaryService
{
    private const int SummaryThreshold = 10;

    private readonly
        IConversationSummarizer _summarizer;

    private readonly
        IConversationRepository _repository;

    public IncrementalSummaryService(
        IConversationSummarizer summarizer,
        IConversationRepository repository)
    {
        _summarizer = summarizer;
        _repository = repository;
    }

    public async Task TrySummarizeAsync(
        Conversation conversation,
        CancellationToken cancellationToken = default)
    {
        var messages =
            conversation.Messages;

        var newMessages =
            messages
                .Skip(conversation.SummarizedMessageCount)
                .Where(x => x.Role != MessageRole.System)
                .ToList();

        if (newMessages.Count < SummaryThreshold)
            return;

        var summary =
            await _summarizer.SummarizeAsync(
                conversation.Summary,
                newMessages,
                cancellationToken);

        conversation.SetSummary(
            summary,
            messages.Count);

        await _repository.UpdateSummaryAsync(
            conversation.Id,
            summary,
            messages.Count,
            cancellationToken);
    }
}