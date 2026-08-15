using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAgent.Application.Abstractions
{
    public interface IAgentTool
    {
        string Name { get; }

        string Description { get; }

        Task<object?> ExecuteAsync(
            IDictionary<string, object?> arguments,
            CancellationToken cancellationToken = default);
    }
}
