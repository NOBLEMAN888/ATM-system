using AtmSystem.Application.Models.AdminUsers;

namespace AtmSystem.Application.Contracts.AdminUsers;

public interface ICurrentAdminUserService
{
    AdminUser? Admin { get; }
}