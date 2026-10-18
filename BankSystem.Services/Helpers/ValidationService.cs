using System.Globalization;
using System.Net.Mail;

namespace BankSystem.Services.Helpers;

public static class ValidationService
{
    public static bool IsCurrencyValid(this string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
        {
            return false;
        }

        foreach (var culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
        {
            try
            {
                if (new RegionInfo(culture.Name).ISOCurrencySymbol == currency)
                {
                    return true;
                }
            }
            catch (ArgumentException)
            {
            }
        }

        return false;
    }

    public static bool IsEmailValid(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        return MailAddress.TryCreate(email, out _);
    }
}
