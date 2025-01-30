namespace AtmSystem.Application.Contracts.Accounts.ResultTypes;

public record GetBalanceResult
{
    private GetBalanceResult() { }

    public sealed record Success(decimal Balance) : GetBalanceResult;

    public sealed record Unauthorized() : GetBalanceResult;
}