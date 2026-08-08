using BuildingBlocks.Domain.Errors;

namespace BuildingBlocks.Domain.Common
{
    public interface IBusinessRule
    {
        bool IsBroken();

        Error Error { get; }
    }
}
