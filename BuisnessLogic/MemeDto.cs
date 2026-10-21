namespace MemeApp.Model
{
    /// <summary>
    /// DTO (Data Transfer Object) для передачи данных о меме из
    /// бизнес-логики (<c>Logic</c>) в интерфейсы — консоль и WinForms.
    /// Ключевое отличие от <see cref="Meme"/>: категория представлена
    /// готовой строкой, полученной через <see cref="EnumHelper.GetDescription"/>.
    /// Благодаря этому интерфейсы не знают про <see cref="EnumHelper"/>
    /// и не дублируют конвертацию enum - string.
    /// </summary>
    public class MemeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsActual { get; set; }
    }
}