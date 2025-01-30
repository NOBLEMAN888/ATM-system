using AtmSystem.Application.Abstractions.Repositories;
using AtmSystem.Application.Contracts.Accounts;
using AtmSystem.Application.Contracts.Accounts.ResultTypes;
using AtmSystem.Application.Models.Accounts;
using AtmSystem.Application.Models.Transactions;
using TransactionResult = AtmSystem.Application.Contracts.Accounts.ResultTypes.TransactionResult;

namespace AtmSystem.Application.ClientUsers;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly CurrentAccountManager _currentAccountManager;

    public AccountService(
        IAccountRepository accountRepository,
        ITransactionRepository transactionRepository,
        CurrentAccountManager currentAccountManager)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
        _currentAccountManager = currentAccountManager;
    }

    public AccountLoginResult Login(long accountNumber, string pin)
    {
        Account? account = _accountRepository.FindAccountByNumberAndPin(accountNumber, pin);

        if (account is null)
        {
            return new AccountLoginResult.NotFound();
        }

        _currentAccountManager.Account = account;
        return new AccountLoginResult.Success();
    }

    public void Logout()
    {
        _currentAccountManager.Account = null;
    }

    public GetBalanceResult GetBalance()
    {
        if (_currentAccountManager.Account is null)
        {
            return new GetBalanceResult.Unauthorized();
        }

        return new GetBalanceResult.Success(_currentAccountManager.Account.Balance);
    }

    public TransactionResult Withdraw(decimal amount)
    {
        if (_currentAccountManager.Account is null)
        {
            return new TransactionResult.Unauthorized();
        }

        if (_currentAccountManager.Account.Balance < amount)
        {
            _transactionRepository.AddTransaction(
                _currentAccountManager.Account.Id,
                _currentAccountManager.Account.Number,
                TransactionOperationType.Withdraw,
                amount,
                Models.Transactions.TransactionResult.Rejected);
            return new TransactionResult.InvalidAmount();
        }

        decimal newBalance = _currentAccountManager.Account.Balance - amount;
        _currentAccountManager.Account = _currentAccountManager.Account with { Balance = newBalance };
        _accountRepository.UpdateAccountBalance(_currentAccountManager.Account.Id, newBalance);

        _transactionRepository.AddTransaction(
            _currentAccountManager.Account.Id,
            _currentAccountManager.Account.Number,
            TransactionOperationType.Withdraw,
            amount,
            Models.Transactions.TransactionResult.Completed);

        return new TransactionResult.Success();
    }

    public TransactionResult Deposit(decimal amount)
    {
        if (_currentAccountManager.Account is null)
        {
            return new TransactionResult.Unauthorized();
        }

        if (amount <= 0)
        {
            _transactionRepository.AddTransaction(
                _currentAccountManager.Account.Id,
                _currentAccountManager.Account.Number,
                TransactionOperationType.Deposit,
                amount,
                Models.Transactions.TransactionResult.Rejected);
            return new TransactionResult.InvalidAmount();
        }

        decimal newBalance = _currentAccountManager.Account.Balance + amount;
        _currentAccountManager.Account = _currentAccountManager.Account with { Balance = newBalance };
        _accountRepository.UpdateAccountBalance(_currentAccountManager.Account.Id, newBalance);

        _transactionRepository.AddTransaction(
            _currentAccountManager.Account.Id,
            _currentAccountManager.Account.Number,
            TransactionOperationType.Deposit,
            amount,
            Models.Transactions.TransactionResult.Completed);

        return new TransactionResult.Success();
    }

    public GetHistoryResult GetTransactionsHistory()
    {
        if (_currentAccountManager.Account is null)
        {
            return new GetHistoryResult.Unauthorized();
        }

        IEnumerable<Transaction> transactions =
            _transactionRepository.FindTransactionsByAccountId(_currentAccountManager.Account.Id);

        if (!transactions.Any())
        {
            return new GetHistoryResult.TransactionsNotFound();
        }

        return new GetHistoryResult.Success(transactions);
    }
}