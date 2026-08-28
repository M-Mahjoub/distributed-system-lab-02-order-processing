using ShoppingAgent.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAgent.Application.AI
{
    public sealed record ChatMessageDto(
         Guid Id,
     MessageRole Role,
     IReadOnlyList<ChatContent> Contents);

    public sealed record ToolResultChatContent(
    string CallId,
    object? Result)
    : ChatContent;
}
