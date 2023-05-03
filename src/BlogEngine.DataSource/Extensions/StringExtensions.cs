namespace BlogEngine.DataSource.Extensions;
public static class StringExtensions
{
    public static string RemoveLeadSlash(this string str)
    {
        if (str.StartsWith("/") || str.StartsWith("\\"))
        {
            return str[1..];
        }

        return str;
    }
}
