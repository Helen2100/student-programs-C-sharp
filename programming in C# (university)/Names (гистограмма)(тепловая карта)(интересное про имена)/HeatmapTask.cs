using System;
using System.Runtime.ExceptionServices;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            //обьявление констант(кол-во дней и кол-ва месяцев)
            var minDay = 2;//так как нам порекомендовали не учитывать людей, родившихся 1 числа
            var maxDay = 31;
            var days = new string[maxDay - 1];//масссив, содержащий дни(не учитываем 1 число)
            var minMonth = 1;
            var maxMonth = 12;
            var months = new string[maxMonth];//массив, содержащий месяцы

            Titles(minDay, days);//присваиваем каждой переменной массива days значение(для пользователя: названия столбцов)
            Titles(minMonth, months);//присваиваем каждой переменной массива months значение(для пользователя: названия строк)

            //двумерный массив, содержащий данные частоты рождаемости людей
            var birthCounts = new double[days.Length, months.Length];
            NameCounter(names, minDay, minMonth, birthCounts);//создаем столбцы

            return new HeatmapData("Карта интенсивностей рождаемости", birthCounts, days, months);
        }
        //создаем сами столбцы
        private static void NameCounter(NameData[] names, int minDay, int minMonth, double[,] birthCounts)
        {
            //цикл, который подсчитывает сколько людей родилось в данный день в данном месяце
            foreach (var count in names)
            {
                //условие, которое не дает вывести программе столбец с кол-вом людей, родившихся 1 числа
                if (count.BirthDate.Day > 1)
                    birthCounts[(count.BirthDate.Day - minDay), (count.BirthDate.Month - minMonth)]++;
            }
        }
        //создаем надписи для строк и столбцов
        private static void Titles(int minTime, string[] times)
        {
            //присваиваем каждой переменной массива days значение
            for (int i = 0; i < times.Length; i++)
                times[i] = (minTime + i).ToString();
        }
    }
}