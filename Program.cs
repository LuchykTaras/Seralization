//Task 1

//using System;
//using System.IO;
//using System.Text.Json;

//namespace FractionSerialization
//{
//    // Структура або клас для дробу
//    public class Fraction
//    {
//        public int Numerator { get; set; }
//        public int Denominator { get; set; }

//        public Fraction() { } // Потрібен для десеріалізації

//        public Fraction(int numerator, int denominator)
//        {
//            if (denominator == 0)
//                throw new ArgumentException("Знаменник не може дорівнювати нулю.");
//            Numerator = numerator;
//            Denominator = denominator;
//        }

//        public override string ToString() => $"{Numerator}/{Denominator}";
//    }

//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.OutputEncoding = System.Text.Encoding.UTF8;
//            Console.InputEncoding = System.Text.Encoding.UTF8;

//            Console.Write("Введіть кількість дробів у масиві: ");
//            if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
//            {
//                Console.WriteLine("Некоректна кількість.");
//                return;
//            }

//            Fraction[] fractions = new Fraction[count];

//            for (int i = 0; i < count; i++)
//            {
//                Console.WriteLine($"\nДріб №{i + 1}:");
//                Console.Write("  Введіть чисельник: ");
//                int num = int.Parse(Console.ReadLine());

//                Console.Write("  Введіть знаменник: ");
//                int den = int.Parse(Console.ReadLine());

//                fractions[i] = new Fraction(num, den);
//            }

//            string filePath = "fractions.json";

//            // 1. Серіалізація та збереження у файл
//            SerializeAndSave(fractions, filePath);

//            // 2. Завантаження та десеріалізація
//            Fraction[] loadedFractions = LoadAndDeserialize(filePath);

//            // Виведення результату для перевірки
//            Console.WriteLine("\n--- Десеріалізований масив дробів із файлу ---");
//            foreach (var fraction in loadedFractions)
//            {
//                Console.WriteLine(fraction);
//            }
//        }

//        static void SerializeAndSave(Fraction[] fractions, string filePath)
//        {
//            // Налаштування для гарного вигляду JSON (відступи)
//            var options = new JsonSerializerOptions { WriteIndented = true };
//            string jsonString = JsonSerializer.Serialize(fractions, options);

//            File.WriteAllText(filePath, jsonString);
//            Console.WriteLine($"\n[Успіх] Масив серіалізовано та збережено у файл: {filePath}");
//        }

//        static Fraction[] LoadAndDeserialize(string filePath)
//        {
//            if (!File.Exists(filePath))
//            {
//                Console.WriteLine("Файл не знайдено!");
//                return Array.Empty<Fraction>();
//            }

//            string jsonString = File.ReadAllText(filePath);
//            Fraction[] fractions = JsonSerializer.Deserialize<Fraction[]>(jsonString);

//            Console.WriteLine("[Успіх] Файл прочитано, дані десеріалізовано.");
//            return fractions;
//        }
//    }
//}

//Task 2, 3
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MagazineSerialization
{
    // Класс статті (Завдання 3)
    public class Article
    {
        public string Title { get; set; }
        public int CharacterCount { get; set; }
        public string Summary { get; set; }

        public Article() { }

        public Article(string title, int characterCount, string summary)
        {
            Title = title;
            CharacterCount = characterCount;
            Summary = summary;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"    - Стаття: \"{Title}\" ({CharacterCount} симв.)");
            Console.WriteLine($"      Анонс: {Summary}");
        }
    }

    // Клас журналу (Завдання 2 + Завдання 3)
    public class Magazine
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int PageCount { get; set; }

        // Список статей вкладений у журнал
        public List<Article> Articles { get; set; } = new List<Article>();

        public Magazine() { }

        public void DisplayInfo()
        {
            Console.WriteLine($"\nЖурнал: \"{Title}\"");
            Console.WriteLine($"Видавництво: {Publisher}");
            Console.WriteLine($"Дата випуску: {ReleaseDate.ToShortDateString()}");
            Console.WriteLine($"Кількість сторінок: {PageCount}");
            Console.WriteLine($"Кількість статей: {Articles.Count}");

            if (Articles.Count > 0)
            {
                Console.WriteLine("Список статей:");
                foreach (var article in Articles)
                {
                    article.DisplayInfo();
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Magazine magazine = new Magazine();

            // 1. Введення інформації про журнал
            Console.WriteLine("--- Введення інформації про журнал ---");
            Console.Write("Назва журналу: ");
            magazine.Title = Console.ReadLine();

            Console.Write("Назва видавництва: ");
            magazine.Publisher = Console.ReadLine();

            Console.Write("Дата випуску (РРРР-ММ-ДД): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                magazine.ReleaseDate = date;
            }
            else
            {
                magazine.ReleaseDate = DateTime.Now;
                Console.WriteLine("Некоректний формат дати. Встановлено поточну дату.");
            }

            Console.Write("Кількість сторінок: ");
            magazine.PageCount = int.Parse(Console.ReadLine());

            // Введення статей (Завдання 3)
            Console.Write("\nСкільки статей додати до журналу? ");
            if (int.TryParse(Console.ReadLine(), out int articleCount) && articleCount > 0)
            {
                for (int i = 0; i < articleCount; i++)
                {
                    Console.WriteLine($"\nВведення статті №{i + 1}:");
                    Console.Write("  Назва статті: ");
                    string aTitle = Console.ReadLine();

                    Console.Write("  Кількість символів: ");
                    int aChars = int.Parse(Console.ReadLine());

                    Console.Write("  Анонс статті: ");
                    string aSummary = Console.ReadLine();

                    magazine.Articles.Add(new Article(aTitle, aChars, aSummary));
                }
            }

            string filePath = "magazine.json";

            // 2. Серіалізація та збереження
            SerializeAndSave(magazine, filePath);

            // 3. Завантаження та десеріалізація
            Magazine loadedMagazine = LoadAndDeserialize(filePath);

            // 4. Виведення інформації про журнал для перевірки
            Console.WriteLine("\n--- Дані, зчитані та десеріалізовані з файлу ---");
            if (loadedMagazine != null)
            {
                loadedMagazine.DisplayInfo();
            }
        }

        static void SerializeAndSave(Magazine magazine, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(magazine, options);

            File.WriteAllText(filePath, jsonString);
            Console.WriteLine($"\n[Успіх] Дані журналу серіалізовано та збережено у: {filePath}");
        }

        static Magazine LoadAndDeserialize(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не знайдено!");
                return null;
            }

            string jsonString = File.ReadAllText(filePath);
            Magazine magazine = JsonSerializer.Deserialize<Magazine>(jsonString);

            Console.WriteLine("[Успіх] Файл журналу десеріалізовано.");
            return magazine;
        }
    }
}

