using A2A;
namespace ShoppingAgent.Infrastructure.A2A;

public sealed class InventoryAgentClient
{
    private readonly A2AClient _client;

    public InventoryAgentClient(
        A2AClient client)
    {
        _client = client;
    }

    public async Task<string> SendAsync(
        string message,
        CancellationToken cancellationToken = default)
    {
        var request =
            new SendMessageRequest
            {
                Message =
                    new Message
                    {
                        MessageId =
                            Guid.NewGuid().ToString("N"),

                        Role =
                            Role.User,

                        Parts =
                        [
                            Part.FromText(message)
                        ]
                    }
            };

        var response =
            await _client.SendMessageAsync(
                request,
                cancellationToken);

        return ExtractResponse(response);
    }

    private static string ExtractResponse(
        SendMessageResponse response)
    {
        switch (response.PayloadCase)
        {
            case SendMessageResponseCase.Message:

                return string.Join(
                    Environment.NewLine,
                    response.Message!
                        .Parts
                        .Where(x => x.Text is not null)
                        .Select(x => x.Text));

            case SendMessageResponseCase.Task:

                return
                    $"Task created: " +
                    response.Task!.Id;

            default:

                return
                    "No response received.";
        }
    }

    public async Task<AgentTask> StartReservationAsync(
       string productId,
       int quantity,
       CancellationToken cancellationToken = default)
    {
        var message =
            new Message
            {
                MessageId =
                    Guid.NewGuid().ToString("N"),

                Role = Role.User,

                Parts =
                [
                    Part.FromText(
                        $"Reserve {quantity} units " +
                        $"of product {productId}.")
                ]
            };


        var request = new SendMessageRequest
        {
            Message = message
        };


        var response =
            await _client.SendMessageAsync(
                request,
                cancellationToken);

        return response.PayloadCase switch
        {
            SendMessageResponseCase.Task =>
                response.Task!,

            SendMessageResponseCase.Message =>
                throw new InvalidOperationException(
                    "InventoryAgent returned a Message instead of a Task."),

            _ =>
                throw new InvalidOperationException(
                    "InventoryAgent returned an empty or unsupported response.")
        };
    }
}