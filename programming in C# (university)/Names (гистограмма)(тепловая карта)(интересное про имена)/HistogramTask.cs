using System;
using System.Linq;


namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var days = new string[31]; //массив с кол-вом дней = столбцы по оси ОХ
            TitlesОХ(days);//создаем подписи для столбцов
            //массив с кол-вом дней рождений в день = высота столбцов на оси ОХ
            var birthsCounts = new double[31];
            NameCounter(names, name, birthsCounts);//создаем столбцы 

            return new HistogramData(String.Format("Рождаемость людей с именем '{0}'", name), days, birthsCounts);
        }
        //создаем подписи для столбцов
        private static void TitlesОХ(string[] days)
        {
            //присваиваем к каждой переменной массива значение(для пользователя: названия столбцов) 
            for (var y = 0; y < 31; y++)
                days[y] = (y + 1).ToString();
        }

        //создаем столбец
        private static void NameCounter(NameData[] names, string name, double[] birthsCounts)
        {
            //цикл, который подсчитывает сколько людей родилось в данный день
            foreach (var day in names)
            {
                //условие, которое не дает вывести программе столбец
                //с кол-вом людей(определенным именем), родившихся 1 числа
                if (day.Name == name && day.BirthDate.Day > 1)
                    birthsCounts[day.BirthDate.Day - 1]++;
            }
        }
    }
}