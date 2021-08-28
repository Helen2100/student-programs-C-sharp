using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;

namespace Greedy
{
    public class GreedyPathFinder : IPathFinder
    {
        public List<Point> FindPathToCompleteGoal(State state)
        {//создаем переменную для записи пути передвижения Жадины
            var history = new List<Point>();
            if (state.Goal == 0) return history;//если списка целей нет, то пустой лист
            var position = state.Position;//запоминаем позицию Жадины
            for (int i = 0; i < state.Goal; i++)//циклом пробекаемся по переменным
            {//создается переменная, в которую мы сразу преобразуем
                var way = new DijkstraPathFinder()//находим пути
                    .GetPathsByDijkstra(state, position, state.Chests)
                    .FirstOrDefault();//запоминаем самый легкий путь
                if (way == null) return history;//возвращаем лист, если такого пути нет
                history.AddRange(way.Path.Skip(1));//добавляем в конец списка путь от этой точки
                position = way.End;//запоминаем позицию Жадины
                state.Energy -= way.Cost;//понижаем "энергию" на трудность пути
                if (state.Energy < 0) return history;//если мы ушли в минус(сразу) возвращаем историю передвижения
                state.Chests.Remove(way.End);//удаляем этот сундук
            }
            return history;//взвращаем результат - историю передвижения
        }
    }
}