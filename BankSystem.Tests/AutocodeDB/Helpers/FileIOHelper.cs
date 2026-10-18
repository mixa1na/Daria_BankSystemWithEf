namespace BankSystem.Tests.AutocodeDB.Helpers;

public static class FileIOHelper
{
    public static readonly string DBDirectory = "DB";

    public static string GetDBFullPath(string file)
    {
        return Path.Combine(DBDirectory, file);
    }
}
