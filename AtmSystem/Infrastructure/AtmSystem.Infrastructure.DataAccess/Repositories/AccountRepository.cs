using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using AtmSystem.Application.Abstractions.Repositories;
using AtmSystem.Application.Models.Accounts;
using Npgsql;

namespace AtmSystem.Infrastructure.DataAccess.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AccountRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public void CreateAccount(long accountNumber, string pin)
    {
        const string sql = $"""
                            insert into accounts (account_number, account_pin, account_balance)
                            values (account_number, account_pin, 0)
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

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection).AddParameter("account_number", accountNumber)
            .AddParameter("account_pin", pin);

        command.ExecuteNonQuery();
    }

    public Account? FindAccountByNumberAndPin(long accountNumber, string pin)
    {
        const string sql = $"""
                            select *
                            from accounts
                            where account_number = :account_number and account_pin = :account_pin
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

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection).AddParameter("account_number", accountNumber)
            .AddParameter("account_pin", pin);
        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
        {
            return null;
        }

        return new Account(
            reader.GetInt64(0),
            reader.GetInt64(1),
            reader.GetString(2),
            reader.GetDecimal(3));
    }

    public Account? FindAccountById(long accountId)
    {
        const string sql = $"""
                            select *
                            from accounts
                            where account_id = :account_id
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

        if (reader.Read() is false)
        {
            return null;
        }

        return new Account(
            reader.GetInt64(0),
            reader.GetInt64(1),
            reader.GetString(2),
            reader.GetDecimal(3));
    }

    public void UpdateAccountBalance(long accountId, decimal newBalance)
    {
        const string sql = $"""
                            update accounts
                            set account_balance = :new_balance
                            where account_id = :account_id
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

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection).AddParameter("new_balance", newBalance)
            .AddParameter("account_id", accountId);

        command.ExecuteNonQuery();
    }
}