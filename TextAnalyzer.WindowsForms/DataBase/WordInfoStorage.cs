using System.Text.Json;
using static iText.Kernel.Pdf.Colorspace.PdfSpecialCs;
using System.Text.RegularExpressions;
namespace TextAnalyzer.WindowsForms;

//Паттерн <<Singleton>>
public class WordInfoStorage
{
    private static WordInfoStorage instance;

    // Объявляем событие
    public event EventHandler WordsChanged;

    private WordInfoStorage(HashSet<WordInfo> initialWords = null)
    {
        words = initialWords ?? new HashSet<WordInfo>();
    }

    private WordInfoStorage() { }

    public static WordInfoStorage GetInstance()
    {
        if (instance == null)
        {
            instance = new WordInfoStorage();
            instance.words = new HashSet<WordInfo>();
        }
        return instance;
    }

    private string searchKey = string.Empty;

    public string SearchKey
    {
        get => searchKey;
        set
        {
            searchKey = value;
            OnWordsChanged();
        }
    }

    private bool strictSearch =false;

    public bool StrictSearch
    {
        get => strictSearch;
        set
        {
            strictSearch = value;
            OnWordsChanged();
        }
    }


    private WordInfo ?filterWord=null;

    public WordInfo? FilterWord
    {
        get => filterWord;
        set
        {
            filterWord = value;
            OnWordsChanged();
        }
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
    public bool RemoveWord(string word, string lemma, string morph_info, string syntax_role)
    {
        WordInfo wordToRemove = words.FirstOrDefault(w =>
            string.Equals(w.word, word, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(w.lemma, lemma, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(w.morph_info, morph_info, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(w.syntax_role, syntax_role, StringComparison.OrdinalIgnoreCase));

        if (wordToRemove != null)
        {
            words.Remove(wordToRemove);
            OnWordsChanged(); // Вызываем событие после удаления слова
            return true;
        }

        return false;
    }

    // Метод для изменения информации о слове
    public bool UpdateWord(WordInfo previousWord, WordInfo updatedWord)
    {
        WordInfo existingWord = words.FirstOrDefault(w => w.Equals(previousWord));
        if (existingWord != null)
        {
            words.Remove(existingWord); // Сначала удаляем старое слово
            words.Add(updatedWord);     // Затем добавляем обновлённое
            OnWordsChanged();           // Вызываем событие после изменения слова
            return true;
        }
        return false;
    }



    public List<WordInfo> GetWords()
    {
        IEnumerable<WordInfo> result = words;

        // Применяем фильтр, если он задан
        if (filterWord != null)
        {
            result = GetRegxWords();
        }

        // Применяем поиск по ключу, если он задан
        if (!string.IsNullOrEmpty(SearchKey)&&!strictSearch)
        {
            result = result.Where(w => w.word.StartsWith(SearchKey, StringComparison.OrdinalIgnoreCase));
        }
        else if(!string.IsNullOrEmpty(SearchKey) && strictSearch)
        {
            result = result.Where(w => w.word==SearchKey);
            strictSearch = false;
        }

        // Возвращаем отсортированный результат
        return result.OrderBy(w => w.word).ToList();
    }

    private List<WordInfo> GetRegxWords()
    {
        return words.Where(w => MatchesFilter(w)).ToList();
    }

    private bool MatchesFilter(WordInfo word)
    {
        return IsMatch(word.word, filterWord.word) &&
               IsMatch(word.lemma, filterWord.lemma) &&
               IsMatch(word.morph_info, filterWord.morph_info) &&
               IsMatch(word.syntax_role, filterWord.syntax_role);
    }

    private bool IsMatch(string value, string ? pattern)
    {
        return string.IsNullOrEmpty(pattern) || Regex.IsMatch(value, pattern);
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
            // Выбрасываем исключение с сообщением об ошибке десериализации
            throw new InvalidOperationException($"Ошибка десериализации JSON: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            // Выбрасываем общее исключение для любых других ошибок
            throw new InvalidOperationException($"Произошла ошибка: {ex.Message}", ex);
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
