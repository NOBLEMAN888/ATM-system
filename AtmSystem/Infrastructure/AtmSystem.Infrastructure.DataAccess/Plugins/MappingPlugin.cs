using Itmo.Dev.Platform.Postgres.Plugins;
using AtmSystem.Application.Models.Transactions;
using Npgsql;

namespace AtmSystem.Infrastructure.DataAccess.Plugins;

public class MappingPlugin : IDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder builder)
    {
        builder.MapEnum<TransactionOperationType>();
        builder.MapEnum<TransactionResult>();
    }
}