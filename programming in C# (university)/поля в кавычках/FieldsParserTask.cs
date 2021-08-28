using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        [TestCase("text", new[] { "text" })]
        [TestCase("hello world", new[] { "hello", "world" })]
        [TestCase("''", new[] { "" })]
        [TestCase("\"bcd ef\"", new[] { "bcd ef" })]
        [TestCase("\"a\'b\'\'c\'d\"", new[] { "a'b''c'd" })]
        [TestCase("'\"1\" \"2\"'", new[] { "\"1\" \"2\"" })]
        [TestCase("a\"bcde\"", new[] { "a", "bcde" })]
        [TestCase("\"bcde\"f", new[] { "bcde", "f" })]
        [TestCase("'abc\\'d'", new[] { "abc'd" })]
        [TestCase(" 5 ", new[] { "5" })]
        [TestCase(@"""v \""d\""""", new[] { @"v ""d""" })]
        [TestCase(@"""\\""", new[] { "\\" })]
        [TestCase("'x ", new[] { "x " })]
        [TestCase("hello   world", new[] { "hello", "world" })]
        [TestCase("", new string[0])]

        public static void RunTests(string input, string[] expectedOutput)
        {
            Test(input, expectedOutput);
        }

        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }
    }

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string line)
        {//возращает итоговые токены
            var token = new List<Token>();//лист для токена
            var index = 0;//позиция с которой нужно начать проверять строчку
            return GetTokenList(line, token, index); // сокращенный синтаксис для инициализации коллекции.
        }

        private static List<Token> GetTokenList(string line, List<Token> token, int index)
        {//метод, который корректирует строку и возвращает токены
            while (index < line.Length)
            {//проверка каждого символа в строке
                if (line[index] == ' ') index++;//избавляемся от пробелов, которые стоят в начале строки
                else
                {
                    token = GetToken(line, token, index);//получаем токен любого поля
                    index = token.Last().GetIndexNextToToken();//увеличиваем индекс
                }
            }
            return token;
        }

        private static List<Token> GetToken(string line, List<Token> token, int index)
        {//метод, который возращает токены
            if (line[index] == '\'' || line[index] == '\"')//если поле начинается с ковычки
                token.Add(ReadQuotedField(line, index));//то вызываем и добавляем токен поля в кавычках(1 практика)
            else
                token.Add(ReadSimplyField(line, index));//иначе вызываем и добавляем токен простого поля
            return token;
        }

        private static Token ReadSimplyField(string line, int startIndex)
        {//метод, который возвращает токен простого поля
            var length = 0;//длина поля
            for (int i = startIndex; i < line.Length; i++)
            {
                if (line[i] == ' ' || line[i] == '\'' || line[i] == '\"')//элементы, которые закрывают поле
                    break;//выходим из цикла
                else length++;//увеличиваем длину строки
            }
            return new Token(line.Substring(startIndex, length), startIndex, length);//возвр токен прост поля
        }
        
        public static Token ReadQuotedField(string line, int startIndex)
        {//метод, который возвращает токен поля в кавычках(1 практика)
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}