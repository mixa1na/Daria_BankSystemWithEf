using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

public class GuidNumberGenerator : IUniqueNumberGenerator
{
    public string Generate()
    {
        return Guid.NewGuid().ToString().GenerateHash();
    }
}
