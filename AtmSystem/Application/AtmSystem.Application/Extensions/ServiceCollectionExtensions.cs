using AtmSystem.Application.AdminUsers;
using AtmSystem.Application.ClientUsers;
using AtmSystem.Application.Contracts.Accounts;
using AtmSystem.Application.Contracts.AdminUsers;
using Microsoft.Extensions.DependencyInjection;

namespace AtmSystem.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IAdminUserService, AdminUserService>();
        collection.AddScoped<IAccountService, AccountService>();

        collection.AddScoped<CurrentAdminUserManager>();
        collection.AddScoped<ICurrentAdminUserService>(
            p => p.GetRequiredService<CurrentAdminUserManager>());
        collection.AddScoped<CurrentAccountManager>();
        collection.AddScoped<ICurrentAccountService>(
            p => p.GetRequiredService<CurrentAccountManager>());

        return collection;
    }
}