using System.Globalization;
using System.Reflection;
using BankSystem.Tests.Entities;
using NUnit.Framework;

namespace BankSystem.Tests.Services;

public class ServiceBaseTests<T> : IDisposable
    where T : class
{
    protected BankContextFactory Factory { get; private set; } = null!;

    protected T Service { get; set; } = null!;

    public void SetUp()
    {
        this.Factory = new BankContextFactory();
    }

    public void ReportService_ReturnsCorrectReportLines(string methodName, Func<T, IReadOnlyList<object>> getActualLines, Func<BankContextFactory, IReadOnlyList<object>> getExpectedLines)
    {
        IReadOnlyList<object> expected = getExpectedLines(this.Factory);
        IReadOnlyList<object> actual = getActualLines(this.Service);

        Assert.That(Rows(actual), Is.EquivalentTo(Rows(expected)), $"{methodName} rows differ from the SQL result.");
        AssertTotalsDescending(actual);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.Factory.Dispose();
        }
    }

    private static List<string> Rows(IReadOnlyList<object> rows) => rows.Select(Row).ToList();

    private static string Row(object row)
    {
        PropertyInfo[] properties = row.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        return string.Join(
            "|",
            properties.Select(property =>
            {
                object? value = property.GetValue(row);
                return value is IFormattable formattable
                    ? formattable.ToString(null, CultureInfo.InvariantCulture)
                    : value?.ToString() ?? string.Empty;
            }));
    }

    private static void AssertTotalsDescending(IReadOnlyList<object> rows)
    {
        if (rows.Count == 0)
        {
            return;
        }

        PropertyInfo? total = rows[0].GetType().GetProperty("Total");
        if (total is null || total.PropertyType != typeof(decimal))
        {
            return;
        }

        decimal previous = decimal.MaxValue;
        foreach (object row in rows)
        {
            var current = (decimal)total.GetValue(row)!;
            Assert.That(current, Is.LessThanOrEqualTo(previous));
            previous = current;
        }
    }
}
