using System.Text.Json;

namespace TextAnalyzer.WindowsForms;


//Паттерн <<Singleton>>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class WordInfoDataBase
{
    public static WordInfoDataBase instance;

    // Объявляем событие
    public event EventHandler WordsChanged;

    private WordInfoDataBase(HashSet<WordInfo> initialWords = null)
    {
        words = initialWords ?? new HashSet<WordInfo>();
    }

    private WordInfoDataBase() { }

    public static WordInfoDataBase GetInstance()
    {
        if (instance == null)
        {
            instance = new WordInfoDataBase();
            instance.words = new HashSet<WordInfo>();
        }
        return instance;
    }

    public static void SubscribeToWordsChanged(EventHandler handler)
    {
        GetInstance().WordsChanged += handler;
    }


    private HashSet<WordInfo> words;

    // Метод для добавления нового слова
    public void AddWord(WordInfo word)
    {
        words.Add(word);
        OnWordsChanged(); // Вызываем событие после добавления слова
    }

    // Метод для удаления слова
    public bool RemoveWord(string word)
    {
        WordInfo wordToRemove = words.FirstOrDefault(w => w.word == word);
        if (wordToRemove != null)
        {
            words.Remove(wordToRemove);
            OnWordsChanged(); // Вызываем событие после удаления слова
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
            OnWordsChanged(); // Вызываем событие после изменения слова
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

    // Статический метод для десериализации списка слов из JSON и обновления существующего экземпляра
    public static void FromJson(string json)
    {
        try
        {
            List<WordInfo> wordList = JsonSerializer.Deserialize<List<WordInfo>>(json);
            var instance = GetInstance(); // Получаем текущий экземпляр
            instance.words.Clear(); // Очищаем текущий список слов
            foreach (var word in wordList)
            {
                instance.words.Add(word); // Добавляем слова из JSON
            }
            instance.OnWordsChanged(); // Оповещаем об изменении списка
        }
        catch (JsonException ex)
        {
            // Обработка ошибок десериализации JSON
            Console.WriteLine($"Ошибка десериализации JSON: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Обработка любых других исключений
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, words.Select(w => w.ToString()));
    }

    // Метод для вызова события
    protected virtual void OnWordsChanged()
    {
        WordsChanged?.Invoke(this, EventArgs.Empty);
    }
}
