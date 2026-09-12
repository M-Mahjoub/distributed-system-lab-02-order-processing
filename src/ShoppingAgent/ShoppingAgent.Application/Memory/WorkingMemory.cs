namespace ShoppingAgent.Application.Memory;

public sealed class WorkingMemory
{
    public string? CurrentTask { get; private set; }

    public Dictionary<string, string> State { get; private set; } = [];

    public void SetTask(string task)
    {
        CurrentTask = task;
    }

    public void Set(
        string key,
        string value)
    {
        State[key] = value;
    }

    public bool TryGet(
        string key,
        out string? value)
    {
        return State.TryGetValue(key, out value);
    }
}