namespace TextAnalyzer.WindowsForms;

public class WordInfo
{
    public string word { get; set; } = string.Empty;
    public string lemma { get; set; } = string.Empty;
    public string morph_info { get; set; } = string.Empty;
    public string syntax_role { get; set; } = string.Empty;

    public WordInfo(string word, string lemma, string morph_info, string syntax_role)
    {
        this.word = word;
        this.lemma = lemma;
        this.morph_info = morph_info;
        this.syntax_role = syntax_role;
    }

    public override string ToString()
    {
        return $"Word: {word}, Lemma: {lemma}, Morph Info: {morph_info}, Syntax Role: {syntax_role}";
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as WordInfo);
    }

    public bool Equals(WordInfo other)
    {
        if (other is null)
            return false;

        // Сравниваем объекты по равенству всех свойств
        return string.Equals(this.word, other.word, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(this.lemma, other.lemma, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(this.morph_info, other.morph_info, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(this.syntax_role, other.syntax_role, StringComparison.OrdinalIgnoreCase);
    }
}