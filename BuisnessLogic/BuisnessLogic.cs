using DataAccessLayer;
using MemeApp.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;




namespace BuisnessLogic
{
    /// <summary>
    /// Класс бизнес-логики приложения. Единственное место, где хранятся
    /// и изменяются данные о мемах. Используется одновременно консолью
    /// и WinForms — интерфейсы не содержат собственной логики.
    /// Наружу отдаёт <see cref="MemeDto"/>, а не <see cref="Meme"/>,
    /// чтобы UI не работал с enum и <see cref="EnumHelper"/>.
    /// </summary>
    public class Logic
    {
        private readonly IRepository<Meme> repository;

        /// <summary>
        /// Подключается к бд
        /// </summary>
        public Logic()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Monsieur-Dramatist\Meme\DataAccessLayer\MemeDatabase.mdf;Integrated Security=True");
            var context = new AppDbContext(optionsBuilder.Options);
            repository = new EntityRepository<Meme>(context);
        }

        /// <summary>
        /// Добавляет новый мем. Проверяет, что название и категория заданы,
        /// и что категория существует.
        /// </summary>
        /// <param name="name">Название мема.</param>
        /// <param name="category">Название категории (описание).</param>
        /// <param name="isActual">Актуален ли мем.</param>
        /// <param name="error">Сообщение об ошибке, если добавление не удалось.</param>
        /// <returns>true, если мем добавлен.</returns>
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

            Meme newMeme = new Meme
            {
                Name = name.Trim(),
                Category = enumCategory,
                IsActual = isActual
            };

            try
            {
                repository.Add(newMeme);
                return true;
            }
            catch (Exception ex)
            {
                error = $"Ошибка БД: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Изменяет данные существующего мема. Проверяет, что мем
        /// существует, что название не пустое и что категория известна.
        /// </summary>
        /// <param name="id">Идентификатор изменяемого мема.</param>
        /// <param name="newName">Новое название.</param>
        /// <param name="newCategory">Новая категория (описание).</param>
        /// <param name="newIsActual">Новая актуальность.</param>
        /// <param name="error">Сообщение об ошибке, если изменение не удалось.</param>
        /// <returns>true, если мем изменён.</returns>
        public bool UpdateMeme(int id, string newName, string newCategory, bool newIsActual, out string error) //Обновление
        {
            error = null;

            Meme meme = repository.ReadById(id);
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
            try
            {
                repository.Update(meme);
                return true;
            }
            catch (Exception ex)
            {
                error = $"Ошибка БД: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Удаляет мем по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор мема.</param>
        /// <param name="error">Сообщение об ошибке, если мем не найден.</param>
        /// <returns>true, если мем удалён.</returns>
        public bool DeleteMeme(int id, out string error) //удаление
        {
            error = null;

            try
            {
                repository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                error = $"Ошибка БД: {ex.Message}";
                return false;
            }
        }





        /// <summary>Возвращает все мемы. Обёртка над <see cref="GetMemes"/>.</summary
        public List<MemeDto> GetAllMemes() => GetMemes();

        /// <summary>
        /// Возвращает мемы выбранной категории.
        /// Обёртка над <see cref="GetMemes"/>.
        /// </summary>
        /// <param name="categoryName">Название категории.</param>
        public List<MemeDto> GetMemesByCategory(string categoryName) => GetMemes(category: categoryName);

        /// <summary>
        /// Возвращает мемы по признаку актуальности.
        /// Обёртка над <see cref="GetMemes"/>.
        /// </summary>
        /// <param name="isActual">true — актуальные, false — неактуальные.</param>
        public List<MemeDto> GetMemesByActualStatus(bool isActual) => GetMemes(onlyActual: isActual);





        //Бизнес функция 1: распределение по категориям
        /// <summary>
        /// Бизнес-функция: распределение мемов по категориям. Возвращает словарь «описание категории → количество мемов».
        /// </summary>
        public Dictionary<string, int> GetCategoryDistribution()
        {
            return repository.Readall()
               .GroupBy(m => m.Category)
               .ToDictionary(g => EnumHelper.GetDescription(g.Key), g => g.Count());
        }

        //для ComboBox
        /// <summary>
        /// Возвращает список описаний всех категорий.
        /// Используется для ComboBox в форме и для меню в консоли.
        /// </summary>
        public List<string> GetCategoryNames()
        {
            return EnumHelper.GetDescriptions<Category>();
        }

        //Текстовая таблица для консоли
        /// <summary>
        /// Бизнес-функция: формирует текстовую таблицу всех мемов
        /// для вывода в консоль.
        /// </summary>
        /// <returns>Строка с таблицей.</returns>
        public string FormatMemesTable()
        {
            var memes = repository.Readall().ToList();
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

        /// <summary>
        /// Единый метод чтения мемов с необязательными фильтрами.
        /// Все остальные методы чтения (<see cref="GetAllMemes"/>,
        /// <see cref="GetMemesByCategory"/>, <see cref="GetMemesByActualStatus"/>)
        /// — тонкие обёртки над этим методом.
        ///
        /// Возвращает <see cref="MemeDto"/>: категория уже преобразована
        /// в строку через <see cref="EnumHelper.GetDescription"/>.
        /// </summary>
        /// <param name="onlyActual">
        /// true — только актуальные; null — без фильтра по актуальности.
        /// </param>
        /// <param name="category">
        /// Название категории; null или пусто — без фильтра по категории.
        /// </param>
        public List<MemeDto> GetMemes(bool? onlyActual = null, string category = null)
        {
            IEnumerable<Meme> query = repository.Readall();

            if (onlyActual == true)
                query = query.Where(m => m.IsActual);

            if (!string.IsNullOrWhiteSpace(category) &&
                EnumHelper.TryParseByDescription<Category>(category, out var cat))
            {
                query = query.Where(m => m.Category == cat);
            }

            return query.Select(m => new MemeDto
            {
                Id = m.Id,
                Name = m.Name,
                Category = EnumHelper.GetDescription(m.Category),
                IsActual = m.IsActual
            }).ToList();
        }

        //Стартовые данные
        /// <summary>
        /// Заполняет список стартовыми демонстрационными мемами.
        /// Вызывается один раз из конструктора.
        /// </summary>
        public void InitializeDatabase()
        {
            if (!repository.Readall().Any())
            {
                repository.Add(new Meme { Name = "НЕвеселый", Category = Category.AIS, IsActual = true });
                repository.Add(new Meme { Name = "ТЕРПИ", Category = Category.University, IsActual = true });
                repository.Add(new Meme { Name = "Чиловый парень", Category = Category.Life, IsActual = true });
                repository.Add(new Meme { Name = "Синий чебупель", Category = Category.Teacher, IsActual = true });
            }
        }
    }
}