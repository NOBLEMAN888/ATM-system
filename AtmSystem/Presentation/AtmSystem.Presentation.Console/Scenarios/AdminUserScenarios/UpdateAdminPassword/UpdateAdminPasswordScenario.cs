using AtmSystem.Application.Contracts.AdminUsers;
using AtmSystem.Application.Contracts.AdminUsers.ResultTypes;
using Spectre.Console;

namespace AtmSystem.Presentation.Console.Scenarios.AdminUserScenarios.UpdateAdminPassword;

public class UpdateAdminPasswordScenario : IScenario
{
    private readonly IAdminUserService _adminUserService;

    public UpdateAdminPasswordScenario(IAdminUserService adminUserService)
    {
        _adminUserService = adminUserService;
    }

    public string Name => "Update administrator password";

    public void Run()
    {
        string newPassword = AnsiConsole.Ask<string>("Enter new admin password: ");

        UpdateAdminPasswordResult result = _adminUserService.UpdatePassword(newPassword);

        string message = result switch
        {
            UpdateAdminPasswordResult.Success => "Password updated successfully",
            UpdateAdminPasswordResult.Unauthorized => "You are not authorized",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}