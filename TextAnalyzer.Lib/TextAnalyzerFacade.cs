using Pullenti.Morph;
using Pullenti.Ner;

namespace TextAnalyzer.Lib;

public static class TextAnalyzerFacade
{
    static Processor processor = ProcessorService.CreateProcessor();

    public static IList<Lexeme> Execute(string text)
    {
        Processor processor = ProcessorService.CreateProcessor();

        AnalysisResult analysisResult = processor.Process(new SourceOfAnalysis(text));

        List<Lexeme> lexemesList = new List<Lexeme>();
        
        for (Token t = analysisResult.FirstToken; t != null; t = t.Next)
        {
            // нетекстовые токены игнорируем
            if (!(t is TextToken)) continue;
            // несуществительные игнорируем
            //if (!t.Morph.Class.IsNoun) continue;

            var formDescription = MorphologyService.GetWordBaseInfo(t.ToString());
            //Console.WriteLine($"Info: {info.ToString()}" );
            var part_of_speach=((Pullenti.Ner.TextToken)t).Morph.Class.ToString();
            string tName= ((Pullenti.Ner.TextToken)t).Term;
            Lexeme newLexeme = new Lexeme(part_of_speach, null,PartOfSpeech.Noun, formDescription.ToString());
            lexemesList.Add(newLexeme);
        }

        return lexemesList;

        //return new List<Lexeme>
        //{
        //    new Lexeme("cat", SentenceMember.Subject, PartOfSpeech.Noun, "Singular"),
        //    new Lexeme("runs", SentenceMember.Predicate, PartOfSpeech.Verb, "Present Tense"),
        //    new Lexeme("quickly", SentenceMember.Adverbial, PartOfSpeech.Adverb, "Manner")
        //};
    }

    public static void Initialize()
    {
        Pullenti.Sdk.InitializeAll();
    }

}
