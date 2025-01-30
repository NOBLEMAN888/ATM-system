using AtmSystem.Application.Contracts.AdminUsers;
using System.Diagnostics.CodeAnalysis;

namespace AtmSystem.Presentation.Console.Scenarios.AdminUserScenarios.CreateAccount;

public class CreateAccountScenarioProvider : IScenarioProvider
{
    private readonly IAdminUserService _service;
    private readonly ICurrentAdminUserService _currentAdmin;

    public CreateAccountScenarioProvider(
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

        scenario = new CreateAccountScenario(_service);
        return true;
    }
}