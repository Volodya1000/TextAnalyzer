namespace TextAnalyzer.Lib;

public record Lexeme(
    string Value,
    SentenceMember? SentenceMember,
    PartOfSpeech PartOfSpeech,
    string FormDescription);