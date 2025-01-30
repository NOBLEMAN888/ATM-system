using AtmSystem.Application.Models.Transactions;

namespace AtmSystem.Application.Abstractions.Repositories;

public interface ITransactionRepository
{
    IEnumerable<Transaction> FindTransactionsByAccountId(long accountId);

    void AddTransaction(
        long accountId,
        long accountNumber,
        TransactionOperationType type,
        decimal amount,
        TransactionResult result);
}