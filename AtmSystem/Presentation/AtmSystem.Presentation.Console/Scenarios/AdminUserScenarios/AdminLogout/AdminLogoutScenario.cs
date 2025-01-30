using AtmSystem.Application.Contracts.AdminUsers;
using Spectre.Console;

namespace AtmSystem.Presentation.Console.Scenarios.AdminUserScenarios.AdminLogout;

public class AdminLogoutScenario : IScenario
{
    private readonly IAdminUserService _adminUserService;

    public AdminLogoutScenario(IAdminUserService adminUserService)
    {
        _adminUserService = adminUserService;
    }

    public string Name => "Logout";

    public void Run()
    {
        _adminUserService.Logout();
        string message = "Logged out successfully";

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}