namespace BankSystem.Services.Models;

public class AccountOwnerTotalBalanceModel
{
    public int AccountOwnerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string CurrencyCode { get; set; } = null!;

    public decimal Total { get; set; }
}
