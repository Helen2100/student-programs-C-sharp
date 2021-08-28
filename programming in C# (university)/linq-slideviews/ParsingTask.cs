using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace linq_slideviews
{
    public class ParsingTask
    {
        /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка заголовочная.</param>
		/// <returns>Словарь: ключ — идентификатор слайда, значение — информация о слайде</returns>
		/// <remarks>Метод должен пропускать некорректные строки, игнорируя их</remarks>
        public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
        {
            return lines.Skip(1)//пропускаем первую строку(название файла)
                    .Select(line => line.Split(';'))//создаем массив подстрок(; условие деления)
                    .Select(SelectLineRecord)//сортируем по условиям описанных в методе
                    .Where(line => line != null)//убраем пустые строки
                    .ToDictionary(line => line.SlideId);//создаем словарь, идентификатор и информацию о слайде
        }

        /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка — заголовочная.</param>
		/// <param name="slides">Словарь информации о слайдах по идентификатору слайда. 
		/// Такой словарь можно получить методом ParseSlideRecords</param>
		/// <returns>Список информации о посещениях</returns>
		/// <exception cref="FormatException">Если среди строк есть некорректные</exception>
        public static IEnumerable<VisitRecord> ParseVisitRecords(
            IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
        {
            return lines.Skip(1)//пропускаем певую строчку(название)
                        .Select(line => SelectLineVisit(slides, line));//сортируем по условиям, описанные в методе
        }

        private static VisitRecord SelectLineVisit(IDictionary<int, SlideRecord> slides, string line)
        {//поиск исключений
            try
            {
                var item = line.Split(';');//делим строку на подстроки=>массив
                return new VisitRecord(//возвращвем переменную, которая содержит в себе следующую информацию
                    int.Parse(item[0]),//идентификатора пользователя типа int(преобразовали из string)
                    int.Parse(item[1]),//идентификатора слайда типа int(преобразовали из string)
                    DateTime.ParseExact(//новая переменная, содержащая инф даты и времени посещения
                        item[2] + ' ' + item[3],//data[2]-дата и data[3]-время
                        "yyyy-MM-dd HH:mm:ss",//формат вывода информации
                        CultureInfo.InvariantCulture,//информация не зависит от региона
                        DateTimeStyles.None),//информация не зависит от стиля
                    slides[int.Parse(item[1])].SlideType);//достаем из словаря информацию по идентификатору слайда
            }
            catch (Exception e)//если мы нашли исключение, то вывести сообщение об исключении
            {
                throw new FormatException($"Wrong line [{line}]", e);//е-содержит в себе информацию об строчке
            }
        }

        private static SlideRecord SelectLineRecord(string[] line)
        {
            int identifier;//переменная для дальнейшей проверки на преобразования элемента строки в число
            SlideType typeTask;//создаем переменную для проверки на преобразовании элемента в эквивалентный объект, не учитвая регистр
            if (line.Length != 3) return null;//если элементов строке не 3, то это ошибка, и нужно присвоить этой строке null
            if (!int.TryParse(line[0], out identifier)) return null;//если не получается, то нужно присвоить этой строке null
            if (!Enum.TryParse(line[1], true, out typeTask)) return null;//Нет, то нужно присвоить этой строке null
            return new SlideRecord(identifier, typeTask, line[2]);//в итоге надо вернуть идентификатор, тип задания и тему занятия
        }
    }
}