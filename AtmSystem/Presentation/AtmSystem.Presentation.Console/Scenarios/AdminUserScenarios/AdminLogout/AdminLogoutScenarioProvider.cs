using AtmSystem.Application.Contracts.AdminUsers;
using System.Diagnostics.CodeAnalysis;

namespace AtmSystem.Presentation.Console.Scenarios.AdminUserScenarios.AdminLogout;

public class AdminLogoutScenarioProvider : IScenarioProvider
{
    private readonly IAdminUserService _service;
    private readonly ICurrentAdminUserService _currentAdmin;

    public AdminLogoutScenarioProvider(
        IAdminUserService service,
        ICurrentAdminUserService currentAdmin)
    {
        _service = service;
        _currentAdmin = currentAdmin;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAdmin.Admin is null)
        {
            scenario = null;
            return false;
        }

        scenario = new AdminLogoutScenario(_service);
        return true;
    }
}