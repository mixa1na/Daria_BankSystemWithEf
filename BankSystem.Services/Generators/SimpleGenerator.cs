using System.Globalization;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

public class SimpleGenerator : IUniqueNumberGenerator
{
    private static readonly Lazy<SimpleGenerator> LazyInstance =
        new(() => new SimpleGenerator());

    private long counter = 1234567890;

    private SimpleGenerator()
    {
    }

    public static SimpleGenerator Instance => LazyInstance.Value;

    public string Generate()
    {
        this.counter++;
        return this.counter.ToString(CultureInfo.InvariantCulture).GenerateHash();
    }
}
