namespace Eshop.RazorPage.Infrastructure.Utils;

public class EnumUtils
{
    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
}