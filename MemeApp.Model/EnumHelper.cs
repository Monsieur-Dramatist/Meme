using System.ComponentModel;
using System.Reflection;

/// <summary>
/// Вспомогательный статический класс для работы с атрибутом
/// <see cref="DescriptionAttribute"/> у значений перечислений.
/// Позволяет конвертировать значение enum ↔ строку-описание.
/// </summary>

public static class EnumHelper
{
    /// <summary>
    /// Возвращает описание значения enum — содержимое атрибута
    /// <see cref="DescriptionAttribute"/>. Если атрибут не задан,
    /// возвращает имя значения.
    /// </summary>
    /// <param name="value">Значение перечисления.</param>
    /// <returns>Строка-описание или имя значения.</returns>
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

    /// <summary>
    /// Возвращает список описаний всех значений перечисления
    /// <typeparamref name="TEnum"/>. Используется для заполнения
    /// ComboBox в форме и меню категорий в консоли.
    /// </summary>
    /// <typeparam name="TEnum">Тип перечисления.</typeparam>
    public static List<string> GetDescriptions<TEnum>() where TEnum : Enum
    {
        List<string> result = new List<string>();
        foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            result.Add(GetDescription(value));
        return result;
    }

    /// <summary>
    /// Пытается найти значение перечисления по его описанию
    /// (без учёта регистра). Используется при вводе категории
    /// пользователем.
    /// </summary>
    /// <typeparam name="TEnum">Тип перечисления.</typeparam>
    /// <param name="description">Строка-описание для поиска.</param>
    /// <param name="result">Найденное значение.</param>
    /// <returns>true, если значение найдено.</returns>
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