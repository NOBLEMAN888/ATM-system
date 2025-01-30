using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using AtmSystem.Application.Abstractions.Repositories;
using AtmSystem.Application.Models.AdminUsers;
using Npgsql;

namespace AtmSystem.Infrastructure.DataAccess.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AdminRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public AdminUser? FindAdminByPassword(string password)
    {
        const string sql = $"""
                            select *
                            from admins
                            where password is :password
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

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection).AddParameter("password", password);
        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
        {
            return null;
        }

        return new AdminUser(
            reader.GetInt64(0),
            reader.GetString(1));
    }

    public void UpdateAdminPassword(long adminId, string newPassword)
    {
        const string sql = $"""
                            update admins
                            set password = :new_password
                            where admin_id = :admin_id
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

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection).AddParameter("new_password", newPassword)
            .AddParameter("admin_id", adminId);

        command.ExecuteNonQuery();
    }
}