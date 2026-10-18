using System.Globalization;
using BankSystem.Services.Generators;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Models.Accounts;

public abstract class BankAccount
{
    private readonly List<AccountCashOperation> operations = new();

    protected BankAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : this(owner, currencyCode, uniqueNumberGenerator, 0m)
    {
    }

    protected BankAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : this(owner, currencyCode, numberGenerator, 0m)
    {
    }

    protected BankAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : this(owner, currencyCode, (uniqueNumberGenerator ?? throw new ArgumentNullException(nameof(uniqueNumberGenerator))).Generate, initialBalance)
    {
    }

    protected BankAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
    {
        ArgumentNullException.ThrowIfNull(owner);
        ArgumentNullException.ThrowIfNull(numberGenerator);

        if (string.IsNullOrWhiteSpace(currencyCode) || !currencyCode.IsCurrencyValid())
        {
            throw new ArgumentException("Currency code is not valid.", nameof(currencyCode));
        }

        this.AccountOwner = owner;
        this.CurrencyCode = currencyCode;
        this.Number = numberGenerator();
        this.Balance = 0m;
        this.BonusPoints = 0;

        owner.Add(this);

        if (initialBalance > 0m)
        {
            this.Deposit(initialBalance, DateTime.Now, "Initial deposit");
        }
    }

    public string Number { get; }

    public decimal Balance { get; private set; }

    public string CurrencyCode { get; }

    public AccountOwner AccountOwner { get; }

    public int BonusPoints { get; private set; }

    protected abstract decimal Overdraft { get; }

    public void Deposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be greater than zero.");
        }

        this.Balance += amount;
        this.operations.Add(new AccountCashOperation(amount, date, note));
        this.BonusPoints += this.CalculateDepositRewardPoints(amount);
    }

    public void Withdraw(decimal amount, DateTime date, string note)
    {
        if (amount <= 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Withdraw amount must be greater than zero.");
        }

        if (amount > this.Balance + this.Overdraft)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        this.Balance -= amount;
        this.operations.Add(new AccountCashOperation(-amount, date, note));
        this.BonusPoints += this.CalculateWithdrawRewardPoints(amount);
    }

    public IList<AccountCashOperation> GetAllOperations()
    {
        return this.operations;
    }

    public override string ToString()
    {
        return string.Format(
            CultureInfo.InvariantCulture,
            "{0} {1} No:{2}. Balance: {3}{4}.\n",
            this.Overdraft,
            this.AccountOwner,
            this.Number,
            this.Balance,
            this.CurrencyCode);
    }

    protected abstract int CalculateDepositRewardPoints(decimal amount);

    protected abstract int CalculateWithdrawRewardPoints(decimal amount);
}
