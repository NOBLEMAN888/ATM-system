using AtmSystem.Application.Abstractions.Repositories;
using AtmSystem.Application.ClientUsers;
using AtmSystem.Application.Contracts.Accounts.ResultTypes;
using AtmSystem.Application.Models.Accounts;
using AtmSystem.Application.Models.Transactions;
using NSubstitute;
using NUnit.Framework;
using Assert = Xunit.Assert;
using TransactionResult = AtmSystem.Application.Models.Transactions.TransactionResult;

namespace AtmSystem.Tests;

public class AtmSystemTests
{
    [Test]
    public void MakeTransaction_Withdraw_WithSufficientBalance_UpdatesBalance_ShouldReturnSuccess()
    {
        // Arrange
        IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
        ITransactionRepository transactionRepository = Substitute.For<ITransactionRepository>();
        var account = new Account(0, 43211234, "1234", 1000m);
        var currentAccountManager = new CurrentAccountManager();
        currentAccountManager.Account = account;
        var accountService =
            new AccountService(accountRepository, transactionRepository, currentAccountManager);
        accountRepository.FindAccountByNumberAndPin(43211234, "1234").Returns(account);

        // Act
        Application.Contracts.Accounts.ResultTypes.TransactionResult withdrawResult = accountService.Withdraw(200m);
        decimal newBalance = 800m;
        GetBalanceResult getBalanceResult = accountService.GetBalance();

        // Assert
        Assert.IsType<Application.Contracts.Accounts.ResultTypes.TransactionResult.Success>(withdrawResult);

        Assert.IsType<GetBalanceResult.Success>(getBalanceResult);
        var successBalance = (GetBalanceResult.Success)getBalanceResult;
        Assert.Equal(newBalance, successBalance.Balance);

        accountRepository.Received(1).UpdateAccountBalance(account.Id, newBalance);
        transactionRepository.Received(1)
            .AddTransaction(
                account.Id,
                account.Number,
                TransactionOperationType.Withdraw,
                200m,
                TransactionResult.Completed);
    }

    [Test]
    public void MakeTransaction_Withdraw_WithInsufficientBalance_ShouldReturnInvalidAmountFailure()
    {
        // Arrange
        IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
        ITransactionRepository transactionRepository = Substitute.For<ITransactionRepository>();
        var account = new Account(0, 43211234, "1234", 1000m);
        var currentAccountManager = new CurrentAccountManager();
        currentAccountManager.Account = account;
        var accountService =
            new AccountService(accountRepository, transactionRepository, currentAccountManager);
        accountRepository.FindAccountByNumberAndPin(43211234, "1234").Returns(account);

        // Act
        Application.Contracts.Accounts.ResultTypes.TransactionResult withdrawResult = accountService.Withdraw(2000m);

        // Assert
        Assert.IsType<Application.Contracts.Accounts.ResultTypes.TransactionResult.InvalidAmount>(withdrawResult);
        transactionRepository.Received(1)
            .AddTransaction(
                account.Id,
                account.Number,
                TransactionOperationType.Withdraw,
                2000m,
                TransactionResult.Rejected);
    }

    [Test]
    public void MakeTransaction_Deposit_UpdatesBalance_ShouldReturnSuccess()
    {
        // Arrange
        IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
        ITransactionRepository transactionRepository = Substitute.For<ITransactionRepository>();
        var account = new Account(0, 43211234, "1234", 1000m);
        var currentAccountManager = new CurrentAccountManager();
        currentAccountManager.Account = account;
        var accountService =
            new AccountService(accountRepository, transactionRepository, currentAccountManager);
        accountRepository.FindAccountByNumberAndPin(43211234, "1234").Returns(account);

        // Act
        Application.Contracts.Accounts.ResultTypes.TransactionResult depositResult = accountService.Deposit(500m);
        decimal newBalance = 1500m;
        GetBalanceResult getBalanceResult = accountService.GetBalance();

        // Assert
        Assert.IsType<Application.Contracts.Accounts.ResultTypes.TransactionResult.Success>(depositResult);

        Assert.IsType<GetBalanceResult.Success>(getBalanceResult);
        var successBalance = (GetBalanceResult.Success)getBalanceResult;
        Assert.Equal(newBalance, successBalance.Balance);

        transactionRepository.Received(1)
            .AddTransaction(
                account.Id,
                account.Number,
                TransactionOperationType.Deposit,
                500m,
                TransactionResult.Completed);
    }
}