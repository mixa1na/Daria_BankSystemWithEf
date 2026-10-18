using System.Globalization;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

public class BasedOnTickUniqueNumberGenerator : IUniqueNumberGenerator
{
    private long lastTicks;

    public BasedOnTickUniqueNumberGenerator(DateTime startingPoint)
    {
        this.startingPoint = startingPoint;
    }

    public DateTime startingPoint { get; }

    public string Generate()
    {
        long ticks = (DateTime.Now - this.startingPoint).Ticks;

        if (ticks <= this.lastTicks)
        {
            ticks = this.lastTicks + 1;
        }

        this.lastTicks = ticks;

        return ticks.ToString(CultureInfo.InvariantCulture).GenerateHash();
    }
}
