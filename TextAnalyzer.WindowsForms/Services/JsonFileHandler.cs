using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
namespace TextAnalyzer.WindowsForms.Services;

public class JsonFileHandler
{
    // Метод для сохранения данных в файл
    public static void SaveToFile(string filePath)
    {
        try
        {
            var database = WordInfoStorage.GetInstance(); // Получаем экземпляр базы данных
            var sortedWords = database.GetWords(); // Получаем отсортированные слова

            // Настраиваем сериализацию для поддержки кириллицы
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };

            string jsonString = JsonSerializer.Serialize(sortedWords, options);
            File.WriteAllText(filePath, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении в файл: {ex.Message}");
        }
    }

    // Метод для загрузки данных из файла
    public static void LoadFromFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                WordInfoStorage.FromJson(jsonString); // Используем существующий метод FromJson для загрузки данных
            }
        }
        catch (InvalidOperationException ex)
        {
            // Обработка ошибки, полученной из функции FromJson
            throw new InvalidOperationException($"{ex.Message}", ex);
        }
    }
}
