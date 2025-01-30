using AtmSystem.Application.Models.Accounts;

namespace AtmSystem.Application.Abstractions.Repositories;

public interface IAccountRepository
{
    void CreateAccount(long accountNumber, string pin);

    Account? FindAccountByNumberAndPin(long accountNumber, string pin);

    Account? FindAccountById(long accountId);

    void UpdateAccountBalance(long accountId, decimal newBalance);
}