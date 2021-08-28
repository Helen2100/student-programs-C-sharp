using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{
	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
			Queue<double> newElements = new Queue<double>();//очередь для заданных чисел
            LinkedList<double> maxElements = new LinkedList<double>();//лист для макс точек
			var times = 0;//счетчик

			foreach (var element in data)
			{//перебираем заданные нам числа
                if (times < windowWidth)//пока окно не заполнено
                    times++;//увеличиваем счетчик

				else if (maxElements.First.Value.Equals(newElements.Dequeue()))
					//или если первый элемент очереди (вход. чисел) равен первому элементу листа (макс)
					maxElements.RemoveFirst();//то удаляем макс чило из листа

                newElements.Enqueue(element.OriginalY);//кладем в очередь чисел

				while ((maxElements.Count > 0) && (element.OriginalY >= maxElements.Last.Value))
					//пока данное число больше последнего макс чилa в листе(и пока лист макс не пуст)
					maxElements.RemoveLast();//то удаляем последнее макс число

				maxElements.AddLast(element.OriginalY);//добавляем число в лист макс
				DataPoint newMaxElement = element.WithMaxY(maxElements.First.Value);//возвращаем тип DataPoint
				yield return newMaxElement;//вывод макс числа
			}
		}
	}
}