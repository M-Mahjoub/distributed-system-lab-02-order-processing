namespace BuildingBlocks.Domain.Errors
{
    public record Error(string Code, ErrorType Type)
    {
        public static readonly Error None =
                                     new(
                                         string.Empty,
                                         ErrorType.None);
    }
}
