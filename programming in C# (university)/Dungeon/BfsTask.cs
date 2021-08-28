using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    public class BfsTask
    {
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
            Queue<Point> queueStartPoint = new Queue<Point>();//инициализируем очередь
            queueStartPoint.Enqueue(start);//добавляем в очередь точки
            HashSet<Point> visitedPoints = new HashSet<Point>();//инициализируем переменную для записи пройденных точек
            visitedPoints.Add(start);//проверка переменной на содержание данной точки
            Dictionary<Point, SinglyLinkedList<Point>> waysTreePoints
                = new Dictionary<Point, SinglyLinkedList<Point>>();//инициализируем очередь
            waysTreePoints.Add(start, new SinglyLinkedList<Point>(start));//доб. в сл. точку(ключ) и лист(значение), который содержит в себе пути
            while (queueStartPoint.Count != 0)//пока в очереди есть точки
            {
                var startPoint = queueStartPoint.Dequeue();//достаем из очереди точку
                if (GetLocationPointMap(map,startPoint)) continue;//проверяем ее раположение на карте
                if (GetInformationPoint(map, startPoint)) continue;//проверяем клетку на содержание(пуста/не пустая)
                for (var y = -1; y <= 1; y++)//пробегаем крадратику 6 на 6
                    for (var x = -1; x <= 1; x++)//по близлежащую точкам
                    {
                        if (x != 0 && y != 0) continue;//если это не т(0;0)(не искомая точка=родитель), то перейти к другой точке
                        var nearPoint = new Point { X = startPoint.X + x, Y = startPoint.Y + y };//запоминаем близлежащую точку
                        if (visitedPoints.Contains(nearPoint)) continue;//если мы уже посещяли эту точку, то перейти к следующей
                        queueStartPoint.Enqueue(nearPoint);//добавляем эту(близлежащую) точку в очередь
                        visitedPoints.Add(nearPoint);//добавляем эту(близлежащую) точку в посещенные
                        waysTreePoints.Add(nearPoint, new SinglyLinkedList<Point>(nearPoint, waysTreePoints[startPoint]));//добавляем точку и ее ветви
                    }
            }
            foreach (var chest in chests)//пробегаем по сундукам
            {
                if (waysTreePoints.ContainsKey(chest))//если мы какая-та из ветвей содержит сундук
                    yield return waysTreePoints[chest];//то возвращаем путь от точки старта до сундука
            }
        }

        //проверяем точку на месторасположение(не вышла она рамки поля)
        public static bool GetLocationPointMap(Map map, Point point) => 
            point.X < 0 || point.X >= map.Dungeon.GetLength(0) || point.Y < 0 || point.Y >= map.Dungeon.GetLength(1);
        //проверяем содержит ли клетка стену
        public static bool GetInformationPoint(Map map, Point startPoint) => 
            map.Dungeon[startPoint.X, startPoint.Y] != MapCell.Empty;
    }
}