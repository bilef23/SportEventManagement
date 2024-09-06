namespace Domain.Helpers;

public class EnumHelper
{
    public static List<string> GetEnumValues<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().Select(e => e.ToString()).ToList();
    }
}