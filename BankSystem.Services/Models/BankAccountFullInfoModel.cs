namespace BankSystem.Services.Models;

public class BankAccountFullInfoModel
{
    public int BankAccountId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string AccountNumber { get; set; } = null!;

    public decimal Balance { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public int BonusPoints { get; set; }
}
