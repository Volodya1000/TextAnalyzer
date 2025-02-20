using System.Text.Json;

namespace TextAnalyzer.WindowsForms.DataBase;

public class WordInfoList
{
    private HashSet<WordInfo> words;

    public WordInfoList(HashSet<WordInfo> initialWords = null)
    {
        words = initialWords ?? new HashSet<WordInfo>();
    }

    // Метод для добавления нового слова
    public void AddWord(WordInfo word)
    {
        words.Add(word);
    }

    // Метод для удаления слова
    public bool RemoveWord(string word)
    {
        WordInfo wordToRemove = words.FirstOrDefault(w => w.word == word);
        if (wordToRemove != null)
        {
            words.Remove(wordToRemove);
            return true;
        }
        return false;
    }

    // Метод для изменения информации о слове
    public bool UpdateWord(string word, WordInfo updatedWord)
    {
        WordInfo existingWord = words.FirstOrDefault(w => w.word == word);
        if (existingWord != null)
        {
            words.Remove(existingWord); // Сначала удаляем старое слово
            words.Add(updatedWord);      // Затем добавляем обновлённое
            return true;
        }
        return false;
    }

    // Метод для получения списка слов, отсортированных по алфавиту
    public List<WordInfo> GetSortedWords()
    {
        return words.OrderBy(w => w.word).ToList();
    }

    // Метод для сериализации списка слов в JSON
    public string ToJson()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(this.words.ToList(), options); // Сериализуем в список
    }

    // Статический метод для десериализации списка слов из JSON
    public static WordInfoList FromJson(string json)
    {
        try
        {
            List<WordInfo> wordList = JsonSerializer.Deserialize<List<WordInfo>>(json);
            return new WordInfoList(new HashSet<WordInfo>(wordList));
        }
        catch (JsonException ex)
        {
            // Обработка ошибок десериализации JSON
            Console.WriteLine($"Ошибка десериализации JSON: {ex.Message}");
            return new WordInfoList();
        }
        catch (Exception ex)
        {
            // Обработка любых других исключений
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
            return new WordInfoList();
        }
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, words.Select(w => w.ToString()));
    }
}