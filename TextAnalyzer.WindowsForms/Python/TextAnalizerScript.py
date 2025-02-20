from natasha import (
    Segmenter,
    MorphVocab,
    NewsEmbedding,
    NewsMorphTagger,
    NewsSyntaxParser,
    Doc
)
import json
import sys
import fitz  # используется библиотека PyMuPDF для чтения pdf файлов


class RussianTranslator:
    def __init__(self):
        self.morph_translations = {
            "Case": {
                "Nom": "Именительный падеж",
                "Gen": "Родительный падеж",
                "Dat": "Дательный падеж",
                "Acc": "Винительный падеж",
                "Ins": "Творительный падеж",
                "Loc": "Предложный падеж"
            },
            "Number": {
                "Sing": "Единственное число",
                "Plur": "Множественное число"
            },
            "Gender": {
                "Masc": "Мужской род",
                "Fem": "Женский род",
                "Neut": "Средний род"
            },
            "Tense": {
                "Past": "Прошедшее время",
                "Pres": "Настоящее время",
                "Fut": "Будущее время"
            },
            "Aspect": {
                "Imp": "Несовершенный вид",
                "Perf": "Совершенный вид"
            },
            "Mood": {
                "Ind": "Изъявительное наклонение",
                "Imp": "Повелительное наклонение"
            },
            "VerbForm": {
                "Fin": "Личная форма",
                "Inf": "Инфинитив",
                "Part": "Причастие",
                "Ger": "Деепричастие"
            },
            "Person": {
                "1": "1-е лицо",
                "2": "2-е лицо",
                "3": "3-е лицо"
            },
            "Animacy": {
                "Anim": "Одушевленный",
                "Inan": "Неодушевленный"
            },
            "Voice": {
                "Act": "Действительный залог",
                "Pass": "Страдательный залог",
                "Mid": "Средний залог"
            },
            "Degree": {
                "Pos": "Положительная степень",
                "Cmp": "Сравнительная степень",
                "Sup": "Превосходная степень"
            },
            "Polarity": {
                "Neg": "Отрицательная полярность"
            }
        }
        self.syntax_translations = {
            "nsubj": "Подлежащее",
            "root": "Сказуемое",
            "obj": "Прямое дополнение",
            "obl": "Обстоятельство",
            "amod": "Согласованное определение",
            "case": "Предлог",
            "cc": "Соединительный союз",
            "conj": "Сочинительная связь",
            "advcl": "Обстоятельственное придаточное предложение",
            "det": "Определитель",
            "punct": "Знак пунктуации",
            "aux": "Вспомогательный глагол",
            "advmod": "Обстоятельственное наречие",
            "xcomp": "Дополнительный член предложения",
            "nummod": "Количественное определение",
            "nmod": "Несогласованное определение",
            "parataxis": "Паратаксис",
            "nummod:gov": "Количественное определение (управляющее слово)",
            "acl": "Определительное придаточное предложение",
            "iobj": "Косвенное дополнение",
            "appos": "Приложение"
        }

    def translate_morph(self, morph_info):
        translated = {}
        for key, value in morph_info.items():
            if key in self.morph_translations and value in self.morph_translations[key]:
                translated[key] = self.morph_translations[key][value]
            else:
                translated[key] = value
        return translated

    def translate_syntax(self, syntax_role):
        return self.syntax_translations.get(syntax_role, syntax_role)


def analyze_text(text):
    segmenter = Segmenter()
    morph_vocab = MorphVocab()
    emb = NewsEmbedding()
    morph_tagger = NewsMorphTagger(emb)
    syntax_parser = NewsSyntaxParser(emb)
    translator = RussianTranslator()

    doc = Doc(text)
    doc.segment(segmenter)
    doc.tag_morph(morph_tagger)
    doc.parse_syntax(syntax_parser)

    words_data = []

    for sent in doc.sents:
        for token in sent.tokens:
            if token.pos != 'PUNCT':
                token.lemmatize(morph_vocab)
                lemma = token.lemma
                morph_info = token.feats
                syntax_role = token.rel

                morph_info_str = ", ".join(translator.translate_morph({key: value})[key] for key, value in morph_info.items()) if morph_info else ""

                syntax_role_str = translator.translate_syntax(syntax_role)

                word_data = {
                    "word": token.text.lower(),
                    "lemma": lemma,
                    "morph_info": morph_info_str,
                    "syntax_role": syntax_role_str
                }
                words_data.append(word_data)

    return json.dumps(words_data, ensure_ascii=False, indent=4)


def extract_text_from_pdf(pdf_path):
    doc = fitz.open(pdf_path)
    text = ""
    for page in doc:
        text += page.get_text()
    return text


if __name__ == "__main__":
    if len(sys.argv) != 3:
        print("Использование: python script.py <1|2> <путь_к_pdf|текст>")
        sys.exit(1)

    arg_type = sys.argv[1]
    second_arg = sys.argv[2]

    if arg_type == '1':
        # Анализируем PDF
        pdf_path = second_arg
        text = extract_text_from_pdf(pdf_path)
    elif arg_type == '2':
        # Анализируем текст
        text = second_arg
    else:
        print("Первый аргумент должен быть 1 или 2.")
        sys.exit(1)

    result_json = analyze_text(text)
    print(result_json)