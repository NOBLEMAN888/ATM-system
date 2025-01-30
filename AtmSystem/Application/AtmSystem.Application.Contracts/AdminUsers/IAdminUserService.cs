using AtmSystem.Application.Contracts.AdminUsers.ResultTypes;

namespace AtmSystem.Application.Contracts.AdminUsers;

public interface IAdminUserService
{
    AdminLoginResult Login(string password);

    void Logout();

    UpdateAdminPasswordResult UpdatePassword(string newPassword);

    CreateAccountResult CreateAccount(long accountNumber, string pin);
}