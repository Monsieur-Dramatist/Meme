namespace MemeApp.Model
{
    /// <summary>
    /// Модель мема. Используется внутри класса <c>Logic</c>
    /// и наружу не отдаётся. Категория представлена перечислением
    /// <see cref="Category"/>, а не строкой.
    /// </summary>
    public class Meme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public bool IsActual { get; set; }
    }
}
