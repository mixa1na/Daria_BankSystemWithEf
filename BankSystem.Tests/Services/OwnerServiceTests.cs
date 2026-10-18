using BankSystem.EF.Entities;
using BankSystem.Services.Models;
using BankSystem.Services.Services;
using NUnit.Framework;

namespace BankSystem.Tests.Services;

[TestFixture]
public class OwnerService : BaseService
{
    public OwnerService(BankContext bankContext)
        : base(bankContext)
    {
    }

    public IReadOnlyList<AccountOwnerTotalBalanceModel> GetAccountOwnersTotalBalance()
    {
        return this.Context.BankAccounts
            .GroupBy(account => new
            {
                account.AccountOwnerId,
                account.AccountOwner.FirstName,
                account.AccountOwner.LastName,
                account.CurrencyCode.CurrenciesCode,
            })
            .Select(group => new AccountOwnerTotalBalanceModel
            {
                AccountOwnerId = group.Key.AccountOwnerId,
                FirstName = group.Key.FirstName,
                LastName = group.Key.LastName,
                CurrencyCode = group.Key.CurrenciesCode,
                Total = group.Sum(account => account.Balance),
            })
            .ToList()
            .OrderByDescending(model => model.Total)
            .ToList();
    }
}
