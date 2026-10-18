using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

public class SilverAccount : BankAccount
{
    private const int SilverDepositCostPerPoint = 5;
    private const int SilverWithdrawCostPerPoint = 2;
    private const int SilverBalanceCostPerPoint = 100;

    public SilverAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : base(owner, currencyCode, uniqueNumberGenerator)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : base(owner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    protected override decimal Overdraft => 2m * this.BonusPoints;

    protected override int CalculateDepositRewardPoints(decimal amount)
    {
        decimal raw = (this.Balance / SilverBalanceCostPerPoint) + (amount / SilverDepositCostPerPoint);
        return (int)Math.Max(Math.Floor(raw), 0m);
    }

    protected override int CalculateWithdrawRewardPoints(decimal amount)
    {
        decimal raw = (this.Balance / SilverBalanceCostPerPoint) + (amount / SilverWithdrawCostPerPoint);
        return (int)Math.Max(Math.Floor(raw), 0m);
    }
}
