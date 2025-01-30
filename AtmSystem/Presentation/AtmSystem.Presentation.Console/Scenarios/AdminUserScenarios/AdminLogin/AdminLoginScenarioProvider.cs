using AtmSystem.Application.Contracts.AdminUsers;
using System.Diagnostics.CodeAnalysis;

namespace AtmSystem.Presentation.Console.Scenarios.AdminUserScenarios.AdminLogin;

public class AdminLoginScenarioProvider : IScenarioProvider
{
    private readonly IAdminUserService _service;
    private readonly ICurrentAdminUserService _currentAdmin;

    public AdminLoginScenarioProvider(
        IAdminUserService service,
        ICurrentAdminUserService currentAdmin)
    {
        _service = service;
        _currentAdmin = currentAdmin;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAdmin.Admin is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new AdminLoginScenario(_service);
        return true;
    }
}