using AtmSystem.Application.Contracts.Accounts;
using System.Diagnostics.CodeAnalysis;

namespace AtmSystem.Presentation.Console.Scenarios.ClientUserScenarios.DepositMoneyToAccount;

public class DepositMoneyToAccountScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _service;
    private readonly ICurrentAccountService _currentAccount;

    public DepositMoneyToAccountScenarioProvider(
        IAccountService service,
        ICurrentAccountService currentAccount)
    {
        _service = service;
        _currentAccount = currentAccount;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccount.Account is null)
        {
            scenario = null;
            return false;
        }

        scenario = new DepositMoneyToAccountScenario(_service);
        return true;
    }
}