using AtmSystem.Application.Contracts.Accounts;
using System.Diagnostics.CodeAnalysis;

namespace AtmSystem.Presentation.Console.Scenarios.ClientUserScenarios.WithdrawMoneyFromAccount;

public class WithdrawMoneyFromAccountScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _service;
    private readonly ICurrentAccountService _currentAccount;

    public WithdrawMoneyFromAccountScenarioProvider(
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

        scenario = new WithdrawMoneyFromAccountScenario(_service);
        return true;
    }
}