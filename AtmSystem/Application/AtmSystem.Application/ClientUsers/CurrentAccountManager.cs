using AtmSystem.Application.Contracts.Accounts;
using AtmSystem.Application.Models.Accounts;

namespace AtmSystem.Application.ClientUsers;

public class CurrentAccountManager : ICurrentAccountService
{
    public Account? Account { get; set; }
}