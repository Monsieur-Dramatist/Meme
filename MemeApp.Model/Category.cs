using System.ComponentModel;

namespace MemeApp.Model
{
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