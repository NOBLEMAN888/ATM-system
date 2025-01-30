namespace AtmSystem.Application.Contracts.AdminUsers.ResultTypes;

public record AdminLoginResult
{
    private AdminLoginResult() { }

    public sealed record Success : AdminLoginResult;

    public sealed record NotFound : AdminLoginResult;
}