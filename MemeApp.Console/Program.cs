using MemeApp.BusinessLogic;
using System;
using System.Collections.Generic;

namespace MemeApp.Console
{
    internal class Program
    {
        static Logic _logic = new Logic();

        static string ShowMainMenu()
        {
            string mainMenuText = "Главное меню:" +
                    "\n1. Добавить новый мем" +
                    "\n2. Удалить мем" +
                    "\n3. Вывести список всех мемов в виде таблицы" +
                    "\n4. Вывести гистограмму распределения мемов по категориям" +
                    "\n5. Выход";
            return mainMenuText;
        }

        static int ReadMenuChoice(int min, int max, string text = "Введите номер пункта: ")
        {
            while (true)
            {
                Console.WriteLine(text);
                string menuUserChoiceString = Console.ReadLine().Trim();

                if (int.TryParse(menuUserChoiceString, out int menuUserChoice)
                    && menuUserChoice >= min && menuUserChoice <= max)
                {
                    return menuUserChoice;
                }
                Console.WriteLine($"Неверный ввод. Введите число от {min} до {max}.");
            }
        }

        static int ReadInt(string text)
        {
            while (true)
            {
                Console.Write(text);
                string input = Console.ReadLine()?.Trim() ?? "";

                if (int.TryParse(input, out int number))
                    return number;

                Console.WriteLine("Неверный ввод. Введите целое число.");
            }
        }

        static bool ReadBool(string text)
        {
            while (true)
            {
                Console.Write(text + " (1 — да, 0 — нет): ");
                string input = Console.ReadLine()?.Trim() ?? "";

                if (input == "1") return true;
                if (input == "0") return false;

                Console.WriteLine("Неверный ввод. Введите 1 или 0.");
            }
        }

        static string AskCategory()
        {
            var categories = _logic.GetCategoryNames();

            Console.WriteLine("Выберите категорию:");
            for (int i = 0; i < categories.Count; i++)
                Console.WriteLine($"{i + 1}. {categories[i]}");

            int index = ReadMenuChoice(1, categories.Count) - 1;
            return categories[index];
        }

        static void Main(string[] args)
        {
            bool isMenuActive = true;
            while (isMenuActive)
            {
                Console.WriteLine(ShowMainMenu());
                int mainMenuUserChoice = ReadMenuChoice(1, 7);

                switch (mainMenuUserChoice)
                {
                    case 1:
                        Console.WriteLine("Добавление нового мема");
                        Console.WriteLine("Введите название мема:");
                        string name = Console.ReadLine().Trim();
                        string categoryForAdd = AskCategory();
                        bool isActualForAdd = ReadBool("Мем актуален?");

                        if (_logic.AddMeme(name, categoryForAdd, isActualForAdd, out string errorInAdd))
                        {
                            Console.WriteLine("Мем успешно добавлен");
                        }
                        else
                        {
                            Console.WriteLine($"Ошибка: {errorInAdd}");
                        }
                        break;

                    case 2:
                        Console.WriteLine("Удаление мема");
                        Console.WriteLine("\nВсе мемы:");
                        Console.WriteLine(_logic.FormatMemesTable());

                        int deleteMenuChoice = ReadInt("Введите id мема для его удаления или 0, чтобы выйти в меню: ");
                        if (deleteMenuChoice == 0)
                        {
                            Console.WriteLine("Вы выбрали выйти в главное меню");
                            break;
                        }
                        if (_logic.DeleteMeme(deleteMenuChoice, out string error))
                        {
                            Console.WriteLine("Мем успешно удалён");
                        }
                        else
                        {
                            Console.WriteLine($"Ошибка: {error}");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Список всех мемов");
                        Console.WriteLine(_logic.FormatMemesTable());
                        break;

                    case 4:
                        Console.WriteLine("Гистограмма распределения мемов по категориям:");
                        Dictionary<string, int> gisto = _logic.GetCategoryDistribution();
                        if (gisto.Count == 0)
                        {
                            Console.WriteLine("Нет мемов");
                        }
                        foreach (KeyValuePair<string, int> pair in gisto)
                        {
                            Console.WriteLine($"{pair.Key}: {pair.Value}");
                        }
                        break;

                    case 5:
                        Console.WriteLine("До новых встреч!");
                        isMenuActive = false;
                        break;
                }
            }
        }
    }
}