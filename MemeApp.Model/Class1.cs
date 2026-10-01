namespace MemeApp.Model
{
    public class Meme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public bool IsActual { get; set; }
    }
}
