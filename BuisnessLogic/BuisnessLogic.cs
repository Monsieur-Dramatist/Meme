using MemeApp.Model;
using System.Text;

namespace BuisnessLogic
{
    public class Logic
    {
        private readonly List<Meme> memes;
        private int nextID = 1;

        public Logic()
        {
            memes = new List<Meme>();
            InitializeMemes();
        }

        public bool AddMeme(string name, string category, bool isActual, out string error) //создать
        {
            error = null;

            if (string.IsNullOrWhiteSpace(name))
            {
                error = "Название не может быть пустым";
                return false;
            }

            if (string.IsNullOrWhiteSpace(category))
            {
                error = "Выберите категорию";
                return false;
            }

            if (!EnumHelper.TryParseByDescription<Category>(category, out var enumCategory))
            {
                error = $"Неизвестная категория: {category}";
                return false;
            }

            memes.Add(new Meme
            {
                Id = nextID++,
                Name = name.Trim(),
                Category = enumCategory,
                IsActual = isActual
            });

            return true;
        }

        public List<MemeDto> GetAllMemes() //прочесть всё
        {
            return memes.Select(m => new MemeDto
            {
                Id = m.Id,
                Name = m.Name,
                Category = EnumHelper.GetDescription(m.Category),
                IsActual = m.IsActual
            }).ToList();
        }

        public List<MemeDto> GetMemesByCategory(string categoryName) //по категориям
        {
            bool found = EnumHelper.TryParseByDescription<Category>(categoryName, out var category);
            if (!found) return new List<MemeDto>();

            return memes
                .Where(m => m.Category == category)
                .Select(m => new MemeDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Category = EnumHelper.GetDescription(m.Category),
                    IsActual = m.IsActual
                })
                .ToList();
        }

        public bool UpdateMeme(int id, string newName, string newCategory, bool newIsActual, out string error) //Обновление
        {
            error = null;

            Meme meme = memes.FirstOrDefault(m => m.Id == id);
            if (meme == null)
            {
                error = "Мем не найден";
                return false;
            }

            if (string.IsNullOrWhiteSpace(newName))
            {
                error = "Название не может быть пустым";
                return false;
            }

            if (!EnumHelper.TryParseByDescription<Category>(newCategory, out var enumCategory))
            {
                error = $"Неизвестная категория: {newCategory}";
                return false;
            }

            meme.Name = newName.Trim();
            meme.Category = enumCategory;
            meme.IsActual = newIsActual;

            return true;
        }

        public bool DeleteMeme(int id, out string error) //удаление
        {
            error = null;

            Meme meme = memes.FirstOrDefault(m => m.Id == id);
            if (meme == null)
            {
                error = "Мем не найден";
                return false;
            }

            memes.Remove(meme);
            return true;
        }

        //Бизнес функция 1: распределение по категориям
        public Dictionary<string, int> GetCategoryDistribution()
        {
            return memes
                .GroupBy(m => m.Category)
                .ToDictionary(g => EnumHelper.GetDescription(g.Key), g => g.Count());
        }

        //Бф 2: только актуальные или нет
        public List<MemeDto> GetMemesByActualStatus(bool isActual)
        {
            return memes
                .Where(m => m.IsActual == isActual)
                .Select(m => new MemeDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Category = EnumHelper.GetDescription(m.Category),
                    IsActual = m.IsActual
                })
                .ToList();
        }

        //для ComboBox
        public List<string> GetCategoryNames()
        {
            return EnumHelper.GetDescriptions<Category>();
        }

        //Текстовая таблица для консоли
        public string FormatMemesTable()
        {
            if (memes.Count == 0)
                return "Список пуст.";

            var rows = memes.Select(m => new
            {
                Id = m.Id.ToString(),
                Name = m.Name,
                Category = EnumHelper.GetDescription(m.Category),
                IsActual = m.IsActual ? "Да" : "Нет"
            }).ToList();

            int idWidth = Math.Max("ID".Length, rows.Max(r => r.Id.Length)) + 2;
            int nameWidth = Math.Max("Название".Length, rows.Max(r => r.Name.Length)) + 2;
            int catWidth = Math.Max("Категория".Length, rows.Max(r => r.Category.Length)) + 2;
            int actWidth = Math.Max("Актуальность".Length, rows.Max(r => r.IsActual.Length)) + 2;

            var sb = new StringBuilder();

            sb.AppendLine(
                "ID".PadRight(idWidth) + " | " +
                "Название".PadRight(nameWidth) + " | " +
                "Категория".PadRight(catWidth) + " | " +
                "Актуальность".PadRight(actWidth));

            int lineWidth = idWidth + nameWidth + catWidth + actWidth + 3 * 3;
            sb.AppendLine(new string('-', lineWidth));

            foreach (var r in rows)
            {
                sb.AppendLine(
                    r.Id.PadRight(idWidth) + " | " +
                    r.Name.PadRight(nameWidth) + " | " +
                    r.Category.PadRight(catWidth) + " | " +
                    r.IsActual.PadRight(actWidth));
            }

            return sb.ToString().TrimEnd();
        }

        //Стартовые данные
        void InitializeMemes()
        {
            AddMeme("НЕвеселый", "АИС", true, out _);
            AddMeme("ТЕРПИ", "Университет", true, out _);
            AddMeme("Чиловый парень", "Жизнь", true, out _);
            AddMeme("Синий чебупель", "Преподы", true, out _);
        }
    }
}