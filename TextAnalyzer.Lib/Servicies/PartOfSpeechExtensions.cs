namespace TextAnalyzer.Lib;

public static class PartOfSpeechExtensions
{
    public static string GetRussianTranslation(this PartOfSpeech partOfSpeech)
    {
        return partOfSpeech switch
        {
            PartOfSpeech.Noun => "Существительное",
            PartOfSpeech.Verb => "Глагол",
            PartOfSpeech.Adjective => "Прилагательное",
            PartOfSpeech.Adverb => "Наречие",
            PartOfSpeech.Pronoun => "Местоимение",
            PartOfSpeech.Preposition => "Предлог",
            PartOfSpeech.Union => "Союз",
            PartOfSpeech.Interjection => "Междометие",
            PartOfSpeech.Numeral => "Числительное",
            _ => "Неизвестно"
        };
    }
}
