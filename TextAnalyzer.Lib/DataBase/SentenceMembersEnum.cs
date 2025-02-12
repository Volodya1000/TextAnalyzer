namespace TextAnalyzer.Lib;

public enum SentenceMember
{
    // Главные члены предложения
    Subject,        // Подлежащее
    Predicate,      // Сказуемое

    // Второстепенные члены предложения
    Attribute,      // Определение
    Object,         // Дополнение
    Adverbial       // Обстоятельство
}
