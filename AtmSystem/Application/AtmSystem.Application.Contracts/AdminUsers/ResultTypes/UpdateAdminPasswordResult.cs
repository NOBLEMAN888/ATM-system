namespace AtmSystem.Application.Contracts.AdminUsers.ResultTypes;

public record UpdateAdminPasswordResult
{
    private UpdateAdminPasswordResult() { }

    public sealed record Success() : UpdateAdminPasswordResult;

    public sealed record Unauthorized() : UpdateAdminPasswordResult;
}