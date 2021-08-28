using System;
using System.Collections.Generic;
using System.Linq;

namespace Passwords
{
    public class CaseAlternatorTask
    {
        //Тесты будут вызывать этот метод
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var words = new List<string>();
            AlternateCharCases(lowercaseWord.ToCharArray(), 0, words);
            return words;
        }

        static void AlternateCharCases(char[] letter, int startIndex, List<string> words)
        {
            var length = letter.Length;//длина слова(массива букв)
            if (startIndex == length)//если пустая строка, или чтобы не выйти за рамки кол-ва букв в словe(при рекурсии)
            {
                string word = new string(letter);//"склеиваем" отдельные символы в слово
                words.Add(word);//добавляем слово(или пустую строку) в лист
                return;//выходим из метода
            }

            letter[startIndex] = IsLetterLow(letter, startIndex, words);//проверка символа и переводим символ в верхний регистр

            AlternateCharCases(letter, startIndex + 1, words);//чтобы добавить измененное слово, или рассматриваем другие варианты
            letter[startIndex] = char.ToLower(letter[startIndex]);//убираем все произведенные изменения
        }

        public static char IsLetterLow(char[] letter, int startIndex, List<string> words)
        {
            bool symbolLetter = char.IsLetter(letter[startIndex]);//проверка символа на принадлежность к буквенному алфовиту
            bool letterLow = (letter[startIndex] == char.ToLower(letter[startIndex]));//проверка символа на нижний регистр
            if (symbolLetter && letterLow)//если буква нижнего регистра
            {
                AlternateCharCases(letter, startIndex + 1, words);//чтобы добавить измененное слово в лист, или рассматриваем др. варианты
                letter[startIndex] = char.ToUpper(letter[startIndex]);//переводим данную букву в верхний регистр
            }
            return letter[startIndex];
        }
    }
}