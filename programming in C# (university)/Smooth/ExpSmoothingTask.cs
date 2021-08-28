using System.Collections.Generic;
using System.Linq;

namespace yield
{
    public static class ExpSmoothingTask
    {
        public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
        {
            var elementNumber = 0;//номер элемента
            var valuePreviousElement = 0.0;//значение предыдущего элемента
            foreach (var element in data)
            {
                elementNumber++;//изменяем номер элемента
                if (elementNumber == 1)//если эл первый
                    valuePreviousElement = element.OriginalY;//изменяем тип этой переменной(для последующих мат.действий)
                else//иначе совершаем мат.действия с этой переменной(параллельно меняя тип переменных)
                    valuePreviousElement = alpha * element.OriginalY + (1 - alpha) * valuePreviousElement;
                var newElement = element.WithExpSmoothedY(valuePreviousElement);//возвращаемся к начальному типу переменной
                yield return newElement;//выводим значение переменной
            }
        }
    }
}