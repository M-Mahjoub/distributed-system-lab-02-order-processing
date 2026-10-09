
using ShoppingAgent.Application.AI;
using ShoppingAgent.Application.Memory;
using ShoppingAgent.Application.Contracts;
using ShoppingAgent.Application.Abstractions;
using ShoppingAgent.Application.Agents;

namespace ShoppingAgent.Application.Conversations;

public sealed class ChatService
{
    private readonly IConversationRepository _conversationRepository;
    private readonly IWorkingMemory _workingMemory;
    private readonly IContextManager _contextManager;
    private readonly IChatModel _chatModel;
    //private readonly IAgentService _agentService;
    private readonly IncrementalSummaryService _summaryService;

    public ChatService(
        IConversationRepository conversationRepository,
        IWorkingMemory workingMemory,
        IContextManager contextManager,
        IChatModel chatModel,
        //IAgentService agentService,
        IncrementalSummaryService summaryService)
    {
        _conversationRepository = conversationRepository;
        _workingMemory = workingMemory;
        _contextManager = contextManager;
        _chatModel = chatModel;
        _summaryService = summaryService;
        //_agentService = agentService;
    }

    public async Task<ChatResponseDto> SendMessageAsync(
        ChatRequest request,
        CancellationToken cancellationToken = default)
    {
        // --------------------------------------------------
        // 1. Load conversation
        // --------------------------------------------------

        var conversation =
            await _conversationRepository.GetAsync(
                request.ConversationId,
                cancellationToken);

        // --------------------------------------------------
        // 2. Create conversation if it doesn't exist
        // --------------------------------------------------

        if (conversation is null)
        {
            conversation =
                new Conversation(
                    request.ConversationId);

            await _conversationRepository.CreateAsync(
                conversation,
                cancellationToken);
        }

        // --------------------------------------------------
        // 3. Add user message
        // --------------------------------------------------

        var userMessage =
            conversation.AddUserMessage(
                request.MessageId,
                request.Message);

        // --------------------------------------------------
        // 4. Persist user message
        // --------------------------------------------------

        var added =
            await _conversationRepository.AppendMessageAsync(
                conversation.Id,
                userMessage,
                cancellationToken);

        if (!added)
        {
            // Message already exists.
            // Idempotency protection.
            return new ChatResponseDto(
                conversation.Id,
                "This message has already been processed.");
        }

        // --------------------------------------------------
        // 5. Load Working Memory
        // --------------------------------------------------

        var workingMemory =
            await _workingMemory.GetAsync(
                conversation.Id,
                cancellationToken);

        // If no working memory exists, create one.
        workingMemory ??= new WorkingMemory();

        // --------------------------------------------------
        // 6. Build LLM Context
        // --------------------------------------------------

        var context =
            _contextManager.BuildContext(
                conversation,
                workingMemory);

        // --------------------------------------------------
        // 7. Ask LLM
        // --------------------------------------------------

        var assistantResponse =
            await _chatModel.GenerateAsync(
                context,
               cancellationToken: cancellationToken);


        //var assistantResponse =
        //                await _agentService.RunAsync(
        //                       context,
        //                       cancellationToken);

        // --------------------------------------------------
        // 8. Add assistant message
        // --------------------------------------------------

        var assistantMessage =
            conversation.AddAssistantMessage(
                assistantResponse);

        // --------------------------------------------------
        // 9. Persist assistant message
        // --------------------------------------------------

        await _conversationRepository.AppendMessageAsync(
            conversation.Id,
            assistantMessage,
            cancellationToken);

        // --------------------------------------------------
        // 10. Save Working Memory
        // --------------------------------------------------

        await _workingMemory.SaveAsync(
            conversation.Id,
            workingMemory,
            cancellationToken);

        // --------------------------------------------------
        // 11. Incremental Summary
        // --------------------------------------------------

        await _summaryService.TrySummarizeAsync(
            conversation,
            cancellationToken);

        // --------------------------------------------------
        // 12. Return API response
        // --------------------------------------------------

        return new ChatResponseDto(
            conversation.Id,
            assistantResponse);
    }
}