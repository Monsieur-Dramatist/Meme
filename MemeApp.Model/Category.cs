using System.ComponentModel;

namespace MemeApp.Model
{
    /// <summary>
    /// Категории мемов. Для каждого значения задано описание через <see cref="DescriptionAttribute"/>, которое
    /// используется для отображения в интерфейсе.
    /// </summary>
    public enum Category
    {
        [Description("Университет")]
        University,
        [Description("АИС")]
        AIS,
        [Description("Преподы")]
        Teacher,
        [Description("Жизнь")]
        Life,
        [Description("Другое")]
        Another,
    }
}