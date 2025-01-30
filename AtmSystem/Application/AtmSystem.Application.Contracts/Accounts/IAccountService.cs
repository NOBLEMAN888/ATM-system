using AtmSystem.Application.Contracts.Accounts.ResultTypes;

namespace AtmSystem.Application.Contracts.Accounts;

public interface IAccountService
{
    AccountLoginResult Login(long accountNumber, string pin);

    void Logout();

    GetBalanceResult GetBalance();

    TransactionResult Withdraw(decimal amount);

    TransactionResult Deposit(decimal amount);

    GetHistoryResult GetTransactionsHistory();
}