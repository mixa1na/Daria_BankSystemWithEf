using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

public class StandardAccount : BankAccount
{
    private const int StandardBalanceCostPerPoint = 100;

    public StandardAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : base(owner, currencyCode, uniqueNumberGenerator)
    {
    }

    public StandardAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    public StandardAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : base(owner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    public StandardAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    protected override decimal Overdraft => 0m;

    protected override int CalculateDepositRewardPoints(decimal amount)
    {
        return (int)Math.Max(Math.Floor(this.Balance / StandardBalanceCostPerPoint), 0m);
    }

    protected override int CalculateWithdrawRewardPoints(decimal amount)
    {
        return (int)Math.Max(Math.Floor(this.Balance / StandardBalanceCostPerPoint), 0m);
    }
}
