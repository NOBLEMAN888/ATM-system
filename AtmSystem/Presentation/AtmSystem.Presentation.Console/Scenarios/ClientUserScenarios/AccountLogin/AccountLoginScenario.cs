using AtmSystem.Application.Contracts.Accounts;
using AtmSystem.Application.Contracts.Accounts.ResultTypes;
using Spectre.Console;

namespace AtmSystem.Presentation.Console.Scenarios.ClientUserScenarios.AccountLogin;

public class AccountLoginScenario : IScenario
{
    private readonly IAccountService _accountService;

    public AccountLoginScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Login as account holder";

    public void Run()
    {
        string number = AnsiConsole.Ask<string>("Enter account number: ");
        string pin = AnsiConsole.Ask<string>("Enter account pin: ");

        if (!long.TryParse(number, out long accountNumber))
        {
            throw new FormatException();
        }

        AccountLoginResult result = _accountService.Login(accountNumber, pin);

        string message = result switch
        {
            AccountLoginResult.Success => "Logged in successfully",
            AccountLoginResult.NotFound => "Account not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}