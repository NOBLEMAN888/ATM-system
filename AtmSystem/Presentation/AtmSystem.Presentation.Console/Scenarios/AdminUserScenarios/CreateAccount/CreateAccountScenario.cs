using AtmSystem.Application.Contracts.AdminUsers;
using AtmSystem.Application.Contracts.AdminUsers.ResultTypes;
using Spectre.Console;

namespace AtmSystem.Presentation.Console.Scenarios.AdminUserScenarios.CreateAccount;

public class CreateAccountScenario : IScenario
{
    private readonly IAdminUserService _adminUserService;

    public CreateAccountScenario(IAdminUserService adminUserService)
    {
        _adminUserService = adminUserService;
    }

    public string Name => "Create new account";

    public void Run()
    {
        string number = AnsiConsole.Ask<string>("Enter account number: ");
        string pin = AnsiConsole.Ask<string>("Enter account pin: ");

        if (!long.TryParse(number, out long accountNumber))
        {
            throw new FormatException();
        }

        CreateAccountResult result = _adminUserService.CreateAccount(accountNumber, pin);

        string message = result switch
        {
            CreateAccountResult.Success => "Account created successfully",
            CreateAccountResult.Unauthorized => "You are not authorized",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}