namespace AtmSystem.Application.Models.Transactions;

public record Transaction(
    long Id,
    long AccountId,
    long AccountNumber,
    TransactionOperationType Type,
    decimal Amount,
    TransactionResult Result);