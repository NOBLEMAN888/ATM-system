using AtmSystem.Application.Contracts.Accounts;
using AtmSystem.Application.Contracts.Accounts.ResultTypes;
using Spectre.Console;

namespace AtmSystem.Presentation.Console.Scenarios.ClientUserScenarios.WithdrawMoneyFromAccount;

public class WithdrawMoneyFromAccountScenario : IScenario
{
    private readonly IAccountService _accountService;

    public WithdrawMoneyFromAccountScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Withdraw money from account";

    public void Run()
    {
        string amount = AnsiConsole.Ask<string>("Enter amount of withdrawal: ");

        if (!decimal.TryParse(amount, out decimal withdrawalAmount))
        {
            throw new FormatException();
        }

        TransactionResult result = _accountService.Withdraw(withdrawalAmount);

        string message = result switch
        {
            TransactionResult.Success => "Withdraw successfully",
            TransactionResult.Unauthorized => "You are not authorized",
            TransactionResult.InvalidAmount => "Invalid amount of deposit",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}