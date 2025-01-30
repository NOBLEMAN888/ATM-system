using AtmSystem.Application.Contracts.Accounts;
using Spectre.Console;

namespace AtmSystem.Presentation.Console.Scenarios.ClientUserScenarios.AccountLogout;

public class AccountLogoutScenario : IScenario
{
    private readonly IAccountService _accountService;

    public AccountLogoutScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Logout";

    public void Run()
    {
        _accountService.Logout();
        string message = "Logged out successfully";

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}