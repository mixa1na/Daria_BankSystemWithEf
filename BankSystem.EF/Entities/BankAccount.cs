using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.EF.Entities;

[Table("bank_account")]
public class BankAccount
{
    [Key]
    [Column("bank_account_id")]
    public int Id { get; set; }

    [Column("account_owner_id")]
    [ForeignKey(nameof(AccountOwner))]
    public int AccountOwnerId { get; set; }

    [Column("account_number")]
    [StringLength(20)]
    public string Number { get; set; }

    [Column("balance")]
    public decimal Balance { get; set; }

    [Column("currency_code_id")]
    [ForeignKey(nameof(CurrencyCode))]
    public int CurrencyCodeId { get; set; }

    [Column("bonus_points")]
    public int BonusPoints { get; set; }

    [Column("overdraft")]
    public decimal Overdraft { get; set; }

    public AccountOwner AccountOwner { get; set; }

    public CurrencyCode CurrencyCode { get; set; }
}
