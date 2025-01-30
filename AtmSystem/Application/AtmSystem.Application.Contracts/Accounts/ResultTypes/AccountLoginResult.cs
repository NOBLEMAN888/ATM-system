namespace AtmSystem.Application.Contracts.Accounts.ResultTypes;

public record AccountLoginResult
{
    private AccountLoginResult() { }

    public sealed record Success : AccountLoginResult;

    public sealed record NotFound : AccountLoginResult;
}