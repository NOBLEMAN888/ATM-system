using AtmSystem.Application.Abstractions.Repositories;
using AtmSystem.Application.Contracts.AdminUsers;
using AtmSystem.Application.Contracts.AdminUsers.ResultTypes;
using AtmSystem.Application.Models.AdminUsers;

namespace AtmSystem.Application.AdminUsers;

internal class AdminUserService : IAdminUserService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly CurrentAdminUserManager _currentAdminUserManager;

    public AdminUserService(
        IAdminRepository adminRepository,
        IAccountRepository accountRepository,
        CurrentAdminUserManager currentAdminUserManager)
    {
        _adminRepository = adminRepository;
        _accountRepository = accountRepository;
        _currentAdminUserManager = currentAdminUserManager;
    }

    public AdminLoginResult Login(string password)
    {
        AdminUser? admin = _adminRepository.FindAdminByPassword(password);

        if (admin is null)
        {
            return new AdminLoginResult.NotFound();
        }

        _currentAdminUserManager.Admin = admin;
        return new AdminLoginResult.Success();
    }

    public void Logout()
    {
        _currentAdminUserManager.Admin = null;
    }

    public UpdateAdminPasswordResult UpdatePassword(string newPassword)
    {
        if (_currentAdminUserManager.Admin is null)
        {
            return new UpdateAdminPasswordResult.Unauthorized();
        }

        _adminRepository.UpdateAdminPassword(_currentAdminUserManager.Admin.Id, newPassword);
        return new UpdateAdminPasswordResult.Success();
    }

    public CreateAccountResult CreateAccount(long accountNumber, string pin)
    {
        if (_currentAdminUserManager.Admin is null)
        {
            return new CreateAccountResult.Unauthorized();
        }

        _accountRepository.CreateAccount(accountNumber, pin);
        return new CreateAccountResult.Success();
    }
}