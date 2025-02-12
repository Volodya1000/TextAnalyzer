namespace TextAnalyzer.Lib;

public static class SentenceMemberExtensions
{
    public static string GetRussianTranslation(this SentenceMember sentenceMember)
    {
        return sentenceMember switch
        {
            SentenceMember.Subject => "Подлежащее",
            SentenceMember.Predicate => "Сказуемое",
            SentenceMember.Attribute => "Определение",
            SentenceMember.Object => "Дополнение",
            SentenceMember.Adverbial => "Обстоятельство",
            _ => "Неизвестно"
        };
    }
}
