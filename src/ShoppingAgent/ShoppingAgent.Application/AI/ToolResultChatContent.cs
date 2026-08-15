using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingAgent.Application.AI
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(TextChatContent), "text")]
    [JsonDerivedType(typeof(ToolResultChatContent), "tool_result")]
    public abstract record ChatContent;

    public sealed record TextChatContent(
    string Text) : ChatContent;

    public sealed record ToolCallChatContent(
    string CallId,
    string Name,
    IReadOnlyDictionary<string, object?> Arguments)
    : ChatContent;
}
