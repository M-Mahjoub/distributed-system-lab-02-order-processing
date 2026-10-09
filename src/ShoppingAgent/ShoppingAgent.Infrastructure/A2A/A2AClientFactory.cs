using A2A;

namespace ShoppingAgent.Infrastructure.A2A;

public sealed class A2AClientFactory
{
    public async Task<A2AClient> CreateAsync(
        string agentUrl,
        CancellationToken cancellationToken = default)
    {
        var resolver =
            new A2ACardResolver(
                new Uri(agentUrl));

        var card =
            await resolver.GetAgentCardAsync(
                cancellationToken);

        var endpoint =
            card
                .SupportedInterfaces
                .First()
                .Url;

        return new A2AClient(
            new Uri(endpoint));
    }
}