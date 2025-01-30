using AtmSystem.Application.Contracts.Accounts;
using System.Diagnostics.CodeAnalysis;

namespace AtmSystem.Presentation.Console.Scenarios.ClientUserScenarios.AccountLogin;

public class AccountLoginScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _service;
    private readonly ICurrentAccountService _currentAccount;

    public AccountLoginScenarioProvider(
        IAccountService service,
        ICurrentAccountService currentAccount)
    {
        _service = service;
        _currentAccount = currentAccount;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccount.Account is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new AccountLoginScenario(_service);
        return true;
    }
}