using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace AtmSystem.Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
    """
    create type transaction_operation_type as enum
    (
        'deposit',
        'withdraw'
    );
    
    create type transaction_result as enum
    (
        'completed',
        'rejected'
    );
    
    create table admins
    (
        admin_id bigint primary key generated always as identity,
        password text not null
    );
    
    create table accounts
    (
        account_id bigint primary key generated always as identity,
        account_number bigint not null,
        account_pin char(4) not null,
        account_balance money not null
    );
    
    create table transactions
    (
        transaction_id bigint primary key generated always as identity,
        account_id bigint not null references accounts(account_id),
        account_number bigint not null,
        transaction_operation_type transaction_operation_type not null,
        operation_amount money not null,
        transaction_result transaction_result not null
    );
    
    insert into admins (password)
    values ('admin_password')
    """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
    """
    drop table admins;
    drop table accounts;
    drop table transactions;

    drop type transaction_operation_type;
    drop type transaction_result;
    """;
}