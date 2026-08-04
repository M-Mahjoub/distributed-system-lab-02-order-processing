namespace BuildingBlocks.Domain
{
    public interface IBusinessRule
    {
        bool IsBroken();

        Error Error { get; }
    }
}
