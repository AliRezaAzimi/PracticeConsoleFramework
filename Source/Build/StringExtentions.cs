using System.Text.RegularExpressions;

namespace PracticeConsoleFramework.Source.Build;

internal static class StringExtentions
{
    public static bool IsValidEmail(this string emailString) =>
        Regex.IsMatch(emailString, @"[a-zA-Z0-9\.-_]+@[a-zA-Z0-9\.-_]+");
}