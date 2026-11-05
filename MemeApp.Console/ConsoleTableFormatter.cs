using DataAccessLayer;
using MemeApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeApp.ConsoleApp
{
    public static class ConsoleTableFormatter
    {
        public static string FormatTable(List<MemeDto> memes)
        {
            if (memes == null || memes.Count == 0)
                return "Список пуст.";

            var rows = memes.Select(m => new
            {
                Id = m.Id.ToString(),
                Name = m.Name,
                Category = m.Category,
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
    }
}
