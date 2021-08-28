using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var listSentences = new List<List<string>>(); // Возвращаемый список
            if (text == null) return null; //проверка на наличие текста
            text = text.ToLower();//перевод в нижний регистр
            char[] endSimbol = ".:;?!()".ToCharArray();//массив символов
            var sentences = text.Split(endSimbol, StringSplitOptions.RemoveEmptyEntries);//делим текст на предложения и убираем пустые предложения

            foreach (var sent in sentences)//берем 1 предложение из массива предложений
            {
                var listWords = new List<string>(); // Список для слов
                var builder = new StringBuilder();//изменяемая строка

                foreach (var ch in sent)
                {
                    if (char.IsLetter(ch) || ch == '\'')//если символ = букве или символ = оппострофу
                        builder.Append(ch);//то добавляем символ в изменяемую строку
                    else
                        AddNotEmptyWord(builder, listWords);//сохраняем результат преобразований в список слов
                }

                AddNotEmptyWord(builder, listWords);//сохраняем результат преобразований в список слов
                if (listWords.Count > 0)//если в листе есть несколько элементов
                    listSentences.Add(listWords);//добовляем лист слов в основной лист
            }

            return listSentences;//выводим лист
        }
        //метод, который сохраняем результат преобразований в список слов
        public static void AddNotEmptyWord(StringBuilder builder, List<string> listWords)
        {
            if (builder.Length > 0)//если изменяемая строка имеет символы
            {
                listWords.Add(builder.ToString());//то преобразовываем все что есть в изменяемой строке в string и добовляем эти переменные в лист слов
                builder.Clear();//удаляем переменные из изменяемой строки
            }
        }
    }
}