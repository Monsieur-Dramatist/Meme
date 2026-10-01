using System.ComponentModel;
using System.Reflection;

public static class EnumHelper
{
    public static string GetDescription(Enum value)
    {
        Type type = value.GetType(); //получает тип (категория)
        string name = value.ToString(); //конкретная категория
        FieldInfo field = type.GetField(name); //ищем конкретное имя
        if (field == null) return name;

        DescriptionAttribute attr = field.GetCustomAttribute<DescriptionAttribute>(); //есть ли дескрипшн а аттр
        if (attr == null) return name;

        return attr.Description;
    }

    public static List<string> GetDescriptions<TEnum>() where TEnum : Enum
    {
        List<string> result = new List<string>();
        foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            result.Add(GetDescription(value));
        return result;
    }

    public static bool TryParseByDescription<TEnum>(string description, out TEnum result)
        where TEnum : struct, Enum
    {
        foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
        {
            string currentDescription = GetDescription(value);
            if (string.Equals(currentDescription, description, StringComparison.OrdinalIgnoreCase))
            {
                result = value;
                return true;
            }
        }
        result = default;
        return false;
    }
}