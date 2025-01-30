using AtmSystem.Application.Contracts.Accounts;
using AtmSystem.Application.Contracts.Accounts.ResultTypes;
using Spectre.Console;

namespace AtmSystem.Presentation.Console.Scenarios.ClientUserScenarios.GetAccountBalance;

public class GetAccountBalanceScenario : IScenario
{
    private readonly IAccountService _accountService;

    public GetAccountBalanceScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "View account balance";

    public void Run()
    {
        GetBalanceResult result = _accountService.GetBalance();

        string message = result switch
        {
            GetBalanceResult.Success success => $"Account balance: {success.Balance}",
            GetBalanceResult.Unauthorized => "You are not authorized",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}