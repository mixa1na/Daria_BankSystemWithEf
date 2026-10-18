using BankSystem.EF.Entities;
using BankSystem.Services.Models;

namespace BankSystem.Services.Services;

public class AccountService : BaseService
{
    public AccountService(BankContext bankContext)
        : base(bankContext)
    {
    }

    public IReadOnlyList<BankAccountFullInfoModel> GetBankAccountsFullInfo()
    {
        return this.Context.BankAccounts
            .Select(account => new BankAccountFullInfoModel
            {
                BankAccountId = account.Id,
                FirstName = account.AccountOwner.FirstName,
                LastName = account.AccountOwner.LastName,
                AccountNumber = account.Number,
                Balance = account.Balance,
                CurrencyCode = account.CurrencyCode.CurrenciesCode,
                BonusPoints = account.BonusPoints,
            })
            .ToList();
    }
}
