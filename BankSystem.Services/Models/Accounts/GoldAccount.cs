using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

public class GoldAccount : BankAccount
{
    private const int GoldDepositCostPerPoint = 10;
    private const int GoldWithdrawCostPerPoint = 5;
    private const int GoldBalanceCostPerPoint = 5;

    public GoldAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : base(owner, currencyCode, uniqueNumberGenerator)
    {
    }

    public GoldAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    public GoldAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : base(owner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    public GoldAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    protected override decimal Overdraft => 3m * this.BonusPoints;

    protected override int CalculateDepositRewardPoints(decimal amount)
    {
        decimal raw = (this.Balance / GoldBalanceCostPerPoint) + (amount / GoldDepositCostPerPoint);
        return (int)Math.Max(Math.Ceiling(raw), 0m);
    }

    protected override int CalculateWithdrawRewardPoints(decimal amount)
    {
        decimal raw = (this.Balance / GoldBalanceCostPerPoint) + (amount / GoldWithdrawCostPerPoint);
        return (int)Math.Max(Math.Ceiling(raw), 0m);
    }
}
