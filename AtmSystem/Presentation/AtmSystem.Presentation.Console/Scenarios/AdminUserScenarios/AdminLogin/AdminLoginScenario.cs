using AtmSystem.Application.Contracts.AdminUsers;
using AtmSystem.Application.Contracts.AdminUsers.ResultTypes;
using Spectre.Console;

namespace AtmSystem.Presentation.Console.Scenarios.AdminUserScenarios.AdminLogin;

public class AdminLoginScenario : IScenario
{
    private readonly IAdminUserService _adminUserService;

    public AdminLoginScenario(IAdminUserService adminUserService)
    {
        _adminUserService = adminUserService;
    }

    public string Name => "Login as administrator";

    public void Run()
    {
        string password = AnsiConsole.Ask<string>("Enter admin password: ");

        AdminLoginResult result = _adminUserService.Login(password);

        string message = result switch
        {
            AdminLoginResult.Success => "Logged in successfully",
            AdminLoginResult.NotFound => "Admin not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}