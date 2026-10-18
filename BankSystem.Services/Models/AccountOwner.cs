using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using BankSystem.Services.Helpers;
using BankSystem.Services.Models.Accounts;

namespace BankSystem.Services.Models;

public class AccountOwner
{
    private readonly List<BankAccount> accounts = new();

    [SuppressMessage("Style", "IDE0032:Use auto property", Justification = "Setter must validate input as required by the specification.")]
    private string firstName = null!;

    [SuppressMessage("Style", "IDE0032:Use auto property", Justification = "Setter must validate input as required by the specification.")]
    private string lastName = null!;

    [SuppressMessage("Style", "IDE0032:Use auto property", Justification = "Setter must validate input as required by the specification.")]
    private string email = null!;

    public AccountOwner(string firstName, string lastName, string email)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
    }

    public string FirstName
    {
        get => this.firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("First name cannot be null, empty or whitespace.", nameof(value));
            }

            this.firstName = value;
        }
    }

    public string LastName
    {
        get => this.lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Last name cannot be null, empty or whitespace.", nameof(value));
            }

            this.lastName = value;
        }
    }

    public string Email
    {
        get => this.email;
        set
        {
            if (value is null || !value.IsEmailValid())
            {
                throw new ArgumentException("Email is not valid.", nameof(value));
            }

            this.email = value;
        }
    }

    public void Add(BankAccount account)
    {
        this.accounts.Add(account);
    }

    public IList<BankAccount> Accounts()
    {
        return this.accounts;
    }

    public override string ToString()
    {
        return string.Format(
            CultureInfo.InvariantCulture,
            "{0} {1}, {2}.",
            this.FirstName,
            this.LastName,
            this.Email);
    }
}
