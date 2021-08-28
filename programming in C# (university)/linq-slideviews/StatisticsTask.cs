using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public class StatisticsTask
    {
        public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
        {
            var listVisits = visits.ToList();//изменяем тип переменной(из IEnumerable в List)
            listVisits.Sort((item1, item2) =>//создаем лямбда-выражение, которая сортирует элементы в listVisits
            {//отражает, расположен экземпляр перед, после или на той же позиции, что и параметр
                var first = item1.UserId.CompareTo(item2.UserId);//сравнение по параметру UserId
                var second = item1.DateTime.CompareTo(item2.DateTime);//сравнение по параметру DateTime
                if (first != 0) return first;//first не имеет ту же позицию в порядке сортировки, что и second.
                else return second;//иначе вернуть second
            });
            var sortResult = listVisits
                .Bigrams()//разбивам переменные по парам
                .Where(data =>//фильтрируем по след.усл: равн UserId и неравн SlideId и SlideType равн образцу
                        (data.Item1.UserId == data.Item2.UserId) && (data.Item1.SlideId != data.Item2.SlideId) && (data.Item1.SlideType == slideType))
                .Select(time => time.Item2.DateTime.Subtract(time.Item1.DateTime).TotalMinutes)//изм.кажд.эл.вход.в послед по усл:разница во времени в мин
                .Where(time => (1.0 <= time) && (time <= 120.0));//фильтрируем по след.усл: время в мин больше 1, но меньше 120
            if (!sortResult.Any()) return 0.0;//если в переменной ничего нет, то венуть ноль
            return sortResult.Median();//иначе вернуть медиану переменной
        }
    }
}