using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public static class ExtensionsTask
	{
		/// <summary>
		/// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
		/// Медиана списка из четного количества элементов — это среднее арифметическое 
        /// двух серединных элементов списка после сортировки.
		/// </summary>
		/// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
		public static double Median(this IEnumerable<double> items)
		{
			List<double> listNumber = new List<double>();//лист для чисел
			foreach(var numbers in items)//пробегаемся по числам
				listNumber.Add(numbers);//добавляем их лист
			if(listNumber.Count != 0)//если лист не пустой
            {
				listNumber.Sort();//то сортируем по умолчанию от большего к меньшему значению
				if (listNumber.Count % 2 == 0)//если четное кол-во чисел
					return (listNumber.ElementAt(listNumber.Count / 2) +
						listNumber.ElementAt((listNumber.Count / 2) - 1)) / 2.0;//возвращаем среднее арифметическое значение
				else 
					return listNumber.ElementAt(listNumber.Count / 2);//возвращаем серединный элемент
			}
			else throw new InvalidOperationException();//иначе возвращаем ошибку
		}

		/// <returns>
		/// Возвращает последовательность, состоящую из пар соседних элементов.
		/// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
		/// </returns>
		public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
		{
			var enumeratorNumber = items.GetEnumerator();//создаем перечислитель
            enumeratorNumber.MoveNext();//перемещаем перечислитель к следующему эл. в коллекции
            var pastValue = enumeratorNumber.Current;//сохраняем текущее значение перечислителя

            while (enumeratorNumber.MoveNext())//пока мы можем перемещать перечислитель
            {//возвращаем пару чисел(предыдущее значенеи и текущее значение)
                yield return Tuple.Create(pastValue, enumeratorNumber.Current);
                pastValue = enumeratorNumber.Current;//сохраняем текущее значение перечислителя
			}
        }
	}
}