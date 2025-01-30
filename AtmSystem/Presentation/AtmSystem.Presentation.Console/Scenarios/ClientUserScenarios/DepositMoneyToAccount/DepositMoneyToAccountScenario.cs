using AtmSystem.Application.Contracts.Accounts;
using AtmSystem.Application.Contracts.Accounts.ResultTypes;
using Spectre.Console;

namespace AtmSystem.Presentation.Console.Scenarios.ClientUserScenarios.DepositMoneyToAccount;

public class DepositMoneyToAccountScenario : IScenario
{
    private readonly IAccountService _accountService;

    public DepositMoneyToAccountScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Deposit money to account";

    public void Run()
    {
        string amount = AnsiConsole.Ask<string>("Enter amount of deposit: ");

        if (!decimal.TryParse(amount, out decimal depositAmount))
        {
            throw new FormatException();
        }

        TransactionResult result = _accountService.Deposit(depositAmount);

        string message = result switch
        {
            TransactionResult.Success => "Deposited successfully",
            TransactionResult.Unauthorized => "You are not authorized",
            TransactionResult.InvalidAmount => "Invalid amount of deposit",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}