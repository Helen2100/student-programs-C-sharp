using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rivals
{
    public class RivalsTask
    {
        public static IEnumerable<OwnedLocation> AssignOwners(Map map)
        {
            var queueStartPoits = new Queue<Tuple<Point, int, int>>();//инициализируем очередь для стартовых точек
            var visitedPoints = new HashSet<Point>();//инициализируем переменную для записи пройденных точек
            for (int i = 0; i < map.Players.Length; i++)//циклом пробегаем по игрокам
            {//создаем точку, в которой находится игрок
                var pointPlayer = new Point(map.Players[i].X, map.Players[i].Y);
                queueStartPoits.Enqueue(Tuple.Create(pointPlayer, i, 0));//кладем информацию о игроке в очередь
            }//координаты, номер игрока и расстояние от начала дерева(0,т.к эти точки=родитель)
            while (queueStartPoits.Count != 0)//пока в очереди есть точки
            {
                var lastPoint = queueStartPoits.Dequeue();//достаем последнюю точку
                var point = lastPoint.Item1;//запоминаем ее координаты
                if (GetLocationPointMap(map, point)) continue;//если она находинся вне карты, то перейти к след точке
                if (GetInformationPoint(map, point)) continue;//если точка = стенке, то перейти к след точке
                if (visitedPoints.Contains(point)) continue;//если эту точку посетитили, то перейти к след точке
                visitedPoints.Add(point);//добавляем эту точку в посещенные
                var owner = lastPoint.Item2;//запоминаем номер игрока
                var location = new Point(point.X, point.Y);//запоминаем месторасположение точки
                var distance = lastPoint.Item3;//запоминаем расстояние до искомой точки(до родителя)
                yield return new OwnedLocation(owner, location, distance);
                for (var y = -1; y <= 1; y++)//пробегаем по квадратику 6 на 6
                    for (var x = -1; x <= 1; x++)//по близлежащую точкам
                        if (x != 0 && y != 0) continue;//если это не т(0;0)(не искомая точка=родитель), то перейти к другой точке
                        else//иначе
                        {//запоминаем координаты точки, находящуюся рядом с искомой
                            var newPoint = new Point { X = point.X + x, Y = point.Y + y };
                            queueStartPoits.Enqueue(Tuple.Create(newPoint, owner, distance + 1));//кладем в очередь инф об новой точке
                        }
            }
        }

        public static bool GetLocationPointMap(Map map, Point point)
        {//проверяем точку на месторасположение(не вышла она рамки поля)
            return point.X < 0 || point.X >= map.Maze.GetLength(0) || point.Y < 0 || point.Y >= map.Maze.GetLength(1);
        }

        public static bool GetInformationPoint(Map map, Point point)
        {//проверяем содержит ли клетка стену
            return map.Maze[point.X, point.Y] == MapCell.Wall;
        }
    }
}
