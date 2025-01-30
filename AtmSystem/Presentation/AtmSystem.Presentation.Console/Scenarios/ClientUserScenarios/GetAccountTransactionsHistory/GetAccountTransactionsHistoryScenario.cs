using AtmSystem.Application.Contracts.Accounts;
using AtmSystem.Application.Contracts.Accounts.ResultTypes;
using AtmSystem.Application.Models.Transactions;
using Spectre.Console;

namespace AtmSystem.Presentation.Console.Scenarios.ClientUserScenarios.GetAccountTransactionsHistory;

public class GetAccountTransactionsHistoryScenario : IScenario
{
    private readonly IAccountService _accountService;

    public GetAccountTransactionsHistoryScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "View account transactions";

    public void Run()
    {
        GetHistoryResult result = _accountService.GetTransactionsHistory();

        string message;
        switch (result)
        {
            case GetHistoryResult.Success success:
                int transactionNumber = success.Transactions.Count();
                foreach (Transaction transaction in success.Transactions.Reverse())
                {
                    string transactionInformation =
                        $"<{transactionNumber}> " +
                        $"Account number: {transaction.AccountNumber}, " +
                        $"Transaction type: {transaction.Type}, " +
                        $"Transaction amount: {transaction.Amount}, " +
                        $"Transaction result: {transaction.Result}";
                    AnsiConsole.WriteLine(transactionInformation);
                    --transactionNumber;
                }

                message = "All transactions have been viewed";
                break;
            case GetHistoryResult.Unauthorized:
                message = "You are not authorized";
                break;
            case GetHistoryResult.TransactionsNotFound:
                message = "Transactions not found";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(result));
        }

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}