using System.Collections.Generic;

namespace yield
{
	public static class MovingAverageTask
	{
		public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var newElement = 0.0;//измененный элемент
			var sumElement = 0.0;//сумма элементов
			Queue<double> newData = new Queue<double>();//очередь для записи новых(изм.) элементов
			foreach (var element in data)
			{
                if (newData.Count != windowWidth)//если элемент входит в окно
                {
                    sumElement += element.OriginalY;//суммируем этот элемент с предыдущими значениями
					newElement = sumElement / (newData.Count + 1);//присваиваем усредненное значение
				}
				else//иначе присваиваем усредненное (по определенной формуле) значение 
					newElement += (element.OriginalY - newData.Dequeue()) / windowWidth;
                var newDataResult = element.WithAvgSmoothedY(newElement);//возвращаемся к начальному типу эл
				yield return newDataResult;//выводим значение новой переменной
				newData.Enqueue(element.OriginalY);//складываем новое значение элемента в очередь
			}
		}
	}
}