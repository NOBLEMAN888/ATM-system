namespace AtmSystem.Application.Contracts.Accounts.ResultTypes;

public record TransactionResult
{
    private TransactionResult() { }

    public sealed record Success() : TransactionResult;

    public sealed record Unauthorized() : TransactionResult;

    public sealed record InvalidAmount() : TransactionResult;
}