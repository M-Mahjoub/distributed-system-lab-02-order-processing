using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAgent.Application.AI
{
    public sealed record ChatResponseDto(
     string? Text,
     IReadOnlyList<ChatToolCallDto> ToolCalls);

    public sealed record ChatToolCallDto(
    string CallId,
    string Name,
    IReadOnlyDictionary<string, object?> Arguments);
}
