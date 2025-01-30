using AtmSystem.Presentation.Console.Scenarios.AdminUserScenarios.AdminLogin;
using AtmSystem.Presentation.Console.Scenarios.AdminUserScenarios.AdminLogout;
using AtmSystem.Presentation.Console.Scenarios.AdminUserScenarios.CreateAccount;
using AtmSystem.Presentation.Console.Scenarios.AdminUserScenarios.UpdateAdminPassword;
using AtmSystem.Presentation.Console.Scenarios.ClientUserScenarios.AccountLogin;
using AtmSystem.Presentation.Console.Scenarios.ClientUserScenarios.AccountLogout;
using AtmSystem.Presentation.Console.Scenarios.ClientUserScenarios.DepositMoneyToAccount;
using AtmSystem.Presentation.Console.Scenarios.ClientUserScenarios.GetAccountBalance;
using AtmSystem.Presentation.Console.Scenarios.ClientUserScenarios.GetAccountTransactionsHistory;
using AtmSystem.Presentation.Console.Scenarios.ClientUserScenarios.WithdrawMoneyFromAccount;
using Microsoft.Extensions.DependencyInjection;

namespace AtmSystem.Presentation.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, AdminLoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AdminLogoutScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CreateAccountScenarioProvider>();
        collection.AddScoped<IScenarioProvider, UpdateAdminPasswordScenarioProvider>();

        collection.AddScoped<IScenarioProvider, AccountLoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AccountLogoutScenarioProvider>();
        collection.AddScoped<IScenarioProvider, DepositMoneyToAccountScenarioProvider>();
        collection.AddScoped<IScenarioProvider, GetAccountBalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, GetAccountTransactionsHistoryScenarioProvider>();
        collection.AddScoped<IScenarioProvider, WithdrawMoneyFromAccountScenarioProvider>();

        return collection;
    }
}