using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    public class DungeonTask
    {
        public static MoveDirection[] FindShortestPath(Map map)
        {//расстояние до выхода
            var wayOut = BfsTask.FindPaths(map, map.InitialPosition, new[] { map.Exit }).FirstOrDefault();
            if (wayOut == null) return new MoveDirection[0];//Если нет пути до выхода, то навпраление=вверх
            if (GetAnyChests(map, wayOut))//Если найденый путь до выхода содержит хоть один сундук
                return wayOut.ToList().ArrayDirections();//массив направлений
            var chests = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);//Находим кратчайший путь до сундука
            var wayDirection = chests.Select(chest => //создаем переменную типа Tuple
                        Tuple.Create(chest, BfsTask.FindPaths(map, chest.Value, new[] { map.Exit }).FirstOrDefault()))
                                     .MinimumDistance();//И ищет в нем минимальный путь
            if (wayDirection == null) //Если кратчайший путь не проходит ни через один сундук
                return wayOut.ToList().ArrayDirections();//массив направлений
            return wayDirection//Парсим каждую часть пути (до сундука и от него) в путь MoveDirection и соединяем
                .Item1.ToList().ArrayDirections()//массив направлений
                .Concat(wayDirection.Item2.ToList().ArrayDirections())//объединяем массив направлений
                .ToArray();//возвращяем массив направлений
        }
        /// <summary>
        /// есть ли путь до выхода, который содержит хоть один сундук
        /// </summary>
        /// <param name="map">карта игры</param>
        /// <param name="wayOut">пути, соединяющий персонажа и выход</param>
        /// <returns>true - есть такой путь, false - нет</returns>
        public static bool GetAnyChests(Map map, SinglyLinkedList<Point> wayOut) =>
            map.Chests.Any(chest => wayOut.ToList().Contains(chest));
    }

    public static class DirectionPointMethods
    {
        /// <summary>
        /// Ищет минимальный путь (путь до сундука и от него до выхода)
        /// </summary>
        /// <param name="ways"></param>
        /// <returns>кратчайших путь</returns>
        public static Tuple<SinglyLinkedList<Point>, SinglyLinkedList<Point>> MinimumDistance
            (this IEnumerable<Tuple<SinglyLinkedList<Point>, SinglyLinkedList<Point>>> ways)
        {//если переменная не содержит нужную информацию
            if (ways.Count() == 0 || ways.First().Item2 == null) return null;
            var minimum = int.MaxValue;//для нахождения мин значения, запоминаем макс знач
            var minimumElement = ways.First();//запоминаем первый элемент последовательности
            foreach (var element in ways)//пробегаем по эл последовательности
                if (element.Item1.Length + element.Item2.Length < minimum)
                {//сумма дорог = минимальная дорога
                    minimum = element.Item1.Length + element.Item2.Length;
                    minimumElement = element;//запоминаем мин эл
                }
            return minimumElement;
        }
        /// <summary>
        /// Перевод из последовательности точек к последовательность направлений
        /// </summary>
        /// <param name="ways">лист содержащий пути</param>
        /// <returns>массив направлений</returns>
        public static MoveDirection[] ArrayDirections(this List<Point> ways)
        {
            var listWays = new List<MoveDirection>();//переменная для записи направлений
            if (ways == null) return new MoveDirection[0];//Если нет пути к выходу, то навпраление=вверх
            for (var i = ways.Count - 1; i > 0; i--)//с помощью цикла
                listWays.Add(GetDirectionMove(ways[i], ways[i - 1]));//добавляем в лист направление между 2 точками
            return listWays.ToArray();
        }
        /// <summary>
        /// Направление между двумя точками
        /// </summary>
        /// <param name="firstPoint">первая точка</param>
        /// <param name="secondPoint">вторая точка</param>
        /// <returns> направление</returns>
        static MoveDirection GetDirectionMove(Point firstPoint, Point secondPoint)
        {//направление зависит от разности координат вектора
            if (firstPoint.Y - secondPoint.Y == 1) return MoveDirection.Up;
            if (firstPoint.Y - secondPoint.Y == -1) return MoveDirection.Down;
            if (firstPoint.X - secondPoint.X == 1) return MoveDirection.Left;
            else return MoveDirection.Right;
        }
    }
}