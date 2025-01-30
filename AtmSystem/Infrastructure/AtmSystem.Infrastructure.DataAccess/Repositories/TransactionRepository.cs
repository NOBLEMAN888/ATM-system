using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using AtmSystem.Application.Abstractions.Repositories;
using AtmSystem.Application.Models.Transactions;
using Npgsql;

namespace AtmSystem.Infrastructure.DataAccess.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public TransactionRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public IEnumerable<Transaction> FindTransactionsByAccountId(long accountId)
    {
        const string sql = $"""
                            select *
                            from transactions
                            where account_id = :account_id
                            order by transaction_id desc
                            """;

        ValueTask<NpgsqlConnection> connectionTask = _connectionProvider
            .GetConnectionAsync(default);

        NpgsqlConnection connection;

        if (connectionTask.IsCompleted)
        {
            connection = connectionTask.Result;
        }
        else
        {
            connection = connectionTask.AsTask().GetAwaiter().GetResult();
        }

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection).AddParameter("account_id", accountId);
        using NpgsqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            yield return new Transaction(
                reader.GetInt64(0),
                reader.GetInt64(1),
                reader.GetInt64(2),
                reader.GetFieldValue<TransactionOperationType>(3),
                reader.GetDecimal(4),
                reader.GetFieldValue<TransactionResult>(5));
        }
    }

    public void AddTransaction(
        long accountId,
        long accountNumber,
        TransactionOperationType type,
        decimal amount,
        TransactionResult result)
    {
        const string sql = $"""
                            insert into transactions 
                                (account_id, account_number, transaction_operation_type, 
                                 operation_amount, transaction_result)
                            values (account_id, account_number, transaction_operation_type, 
                                    operation_amount, transaction_result)
                            """;

        ValueTask<NpgsqlConnection> connectionTask = _connectionProvider
            .GetConnectionAsync(default);

        NpgsqlConnection connection;

        if (connectionTask.IsCompleted)
        {
            connection = connectionTask.Result;
        }
        else
        {
            connection = connectionTask.AsTask().GetAwaiter().GetResult();
        }

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection).AddParameter("account_id", accountId)
            .AddParameter("account_number", accountNumber)
            .AddParameter("transaction_operation_type", type)
            .AddParameter("operation_amount", amount)
            .AddParameter("transaction_result", result);

        command.ExecuteNonQuery();
    }
}