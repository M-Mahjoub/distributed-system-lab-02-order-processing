using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingAgent.Application.AI;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShoppingAgent.Application.Abstractions
{
    public interface IChatModel
    {
        Task<ChatResponseDto> GenerateAsync(
            IEnumerable<ChatMessageDto> messages,
            IEnumerable<IAgentTool> tools,
            CancellationToken cancellationToken = default);
    }
}
