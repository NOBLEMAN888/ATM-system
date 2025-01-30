namespace AtmSystem.Application.Contracts.AdminUsers.ResultTypes;

public record CreateAccountResult
{
    private CreateAccountResult() { }

    public sealed record Success() : CreateAccountResult;

    public sealed record Unauthorized() : CreateAccountResult;
}