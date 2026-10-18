using NUnit.Framework;

namespace BankSystem.Tests.Entities;

[TestFixture]
public sealed class BankContextTests
{
    [Test]
    public void DbSets_ContainTheFixtureRows()
    {
        using var factory = new BankContextFactory();
        using var context = factory.CreateContext();

        Assert.Multiple(() =>
        {
            Assert.That(context.AccountOwners.Count(), Is.EqualTo(3));
            Assert.That(context.CurrencyCodes.Count(), Is.EqualTo(20));
            Assert.That(context.BankAccounts.Count(), Is.EqualTo(5));
            Assert.That(context.AccountCashOperations.Count(), Is.EqualTo(15));
        });
    }
}
