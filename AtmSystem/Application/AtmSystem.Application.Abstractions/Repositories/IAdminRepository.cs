using AtmSystem.Application.Models.AdminUsers;

namespace AtmSystem.Application.Abstractions.Repositories;

public interface IAdminRepository
{
    AdminUser? FindAdminByPassword(string password);

    void UpdateAdminPassword(long adminId, string newPassword);
}