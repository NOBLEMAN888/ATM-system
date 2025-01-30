using AtmSystem.Application.Contracts.AdminUsers;
using AtmSystem.Application.Models.AdminUsers;

namespace AtmSystem.Application.AdminUsers;

internal class CurrentAdminUserManager : ICurrentAdminUserService
{
    public AdminUser? Admin { get; set; }
}