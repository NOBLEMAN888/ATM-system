using AtmSystem.Application.Models.Transactions;

namespace AtmSystem.Application.Contracts.Accounts.ResultTypes;

public record GetHistoryResult
{
    private GetHistoryResult() { }

    public sealed record Success(IEnumerable<Transaction> Transactions) : GetHistoryResult;

    public sealed record Unauthorized() : GetHistoryResult;

    public sealed record TransactionsNotFound() : GetHistoryResult;
}