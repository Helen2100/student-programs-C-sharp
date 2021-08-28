using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("''", 0, "", 2)]
        [TestCase("''qwerty'", 1, "qwerty", 8)]
        [TestCase("''abc\\'d\"d'", 1, "abc'd\"d", 10)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var builder = new StringBuilder();//переменная,в которой будет новая строка
            char[] symbol = line.ToCharArray();//создаем массив символов строки line
            var countSpecialSymbol = 1;//кол-во специальных символов(+1,т.к всегда есть открыв.ковычка)
            var start = startIndex + 1;//+1, чтобы не выйти за границы цикла
            for (var i = start; i < symbol.Length; i++)//просматривам каждый элемент строки
            {
                if (symbol[i] == symbol[startIndex] && symbol[i - 1] != '\\')//если симв = ковычке и не явл экран.симв
                {
                    countSpecialSymbol++;//счетчик кол-ва ковычек
                    break;
                }
                else
                {
                    builder.Append(symbol[i]);//добавляем символ в строку
                    if (symbol[i - 1] == '\\')//если симв = экран.симв
                        countSpecialSymbol++;//счетчик кол-во экран.симв
                }
            }
            builder.Replace("\\", "");//заменяем \ на пустой элемент => удаляем элемент из строки
            var newLine = builder.ToString();//изменяем тип строки
            var length = newLine.Length + countSpecialSymbol;//длина новой строки
            return new Token(newLine, startIndex, length);//вывод строки, начального индекса, длины строки
        }
    }
}
