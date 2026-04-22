using System.Globalization;

namespace InternetClub.RazorPages.Utils;

public static class DateInt
{
    private static readonly CultureInfo Ru = CultureInfo.GetCultureInfo("ru-RU");

    public static bool TryParseRu(string? text, out int yyyymmdd)
    {
        yyyymmdd = default;

        if (string.IsNullOrWhiteSpace(text))
        {
            return false;
        }

        if (!DateOnly.TryParseExact(text.Trim(), "dd.MM.yyyy", Ru, DateTimeStyles.None, out var date))
        {
            return false;
        }

        yyyymmdd = date.Year * 10000 + date.Month * 100 + date.Day;
        return true;
    }

    public static string? Format(int yyyymmdd)
    {
        if (yyyymmdd <= 0)
        {
            return null;
        }

        var year = yyyymmdd / 10000;
        var month = (yyyymmdd / 100) % 100;
        var day = yyyymmdd % 100;

        if (!DateOnly.TryParseExact($"{day:00}.{month:00}.{year:0000}", "dd.MM.yyyy", Ru, DateTimeStyles.None,
                out var date))
        {
            return yyyymmdd.ToString(CultureInfo.InvariantCulture);
        }

        return date.ToString("dd.MM.yyyy", Ru);
    }
}

