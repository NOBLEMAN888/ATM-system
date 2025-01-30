using AtmSystem.Application.Models.Accounts;

namespace AtmSystem.Application.Contracts.Accounts;

public interface ICurrentAccountService
{
    Account? Account { get; }
}